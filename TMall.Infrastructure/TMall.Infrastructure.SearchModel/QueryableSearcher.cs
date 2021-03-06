﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TMall.Infrastructure.SearchModel.TransformProviders;

namespace TMall.Infrastructure.SearchModel
{
    internal class QueryableSearcher<T>
    {
        public QueryableSearcher()
        {
            Init();
        }
        public QueryableSearcher(IQueryable<T> table, ICollection<ConditionItem> items)
            : this()
        {
            Table = table;
            Items = items;
        }
        private void Init()
        {
            TransformProviders = new List<ITransformProvider>
                                     {
                                         new LikeTransformProvider(),
                                         new DateBlockTransformProvider(),
                                         new InTransformProvider(),
                                         new UnixTimeTransformProvider()
                                     };
        }



        public List<ITransformProvider> TransformProviders { get; set; }
        protected ICollection<ConditionItem> Items { get; set; }

        protected IQueryable<T> Table { get; set; }

        public IQueryable<T> Search()
        {
            //构建 c=>Body中的c
            ParameterExpression param = Expression.Parameter(typeof(T), "c");
            //构建c=>Body中的Body
            var body = GetExpressoinBody(param, Items);
            //将二者拼为c=>Body
            var expression = Expression.Lambda<Func<T, bool>>(body, param);
            //传到Where中当做参数，类型为Expression<Func<T,bool>>
            return Table.Where(expression);
        }

        private Expression GetExpressoinBody(ParameterExpression param, ICollection<ConditionItem> items)
        {
            var list = new List<Expression>();
            //OrGroup为空的情况下，即为And组合
            var andList = items.Where(c => string.IsNullOrEmpty(c.OrGroup)).ToArray();
            //将And的子Expression以AndAlso拼接
            if (andList.Count() != 0)
            {
                list.Add(GetGroupExpression(param, andList, Expression.AndAlso));
            }
            //其它的则为Or关系，不同Or组间以And分隔
            var orGroupByList = items.Where(c => !string.IsNullOrEmpty(c.OrGroup)).GroupBy(c => c.OrGroup);
            //拼接子Expression的Or关系
            var collection = orGroupByList
                .Where(grouping => grouping.Count() != 0)
                .Select(grouping => GetGroupExpression(param, grouping, Expression.OrElse));
            list.AddRange(collection);
            //将这些Expression再以And相连
            return list.Aggregate(Expression.AndAlso);
        }

        private Expression GetGroupExpression(ParameterExpression param, IEnumerable<ConditionItem> items, Func<Expression, Expression, Expression> func)
        {
            //获取最小的判断表达式
            var list = items.Select(item => GetExpression(param, item));
            //再以逻辑运算符相连
            return list.Aggregate(func);
        }

        private Expression GetExpression(ParameterExpression param, ConditionItem item)
        {
            //属性表达式
            LambdaExpression exp = GetPropertyLambdaExpression(item, param);
            //如果有特殊类型处理，则进行处理，暂时不关注
            foreach (var provider in TransformProviders)
            {
                if (provider.Match(item, exp.Body.Type))
                {
                    return GetGroupExpression(param, provider.Transform(item, exp.Body.Type), Expression.AndAlso);
                }
            }
            //常量表达式
            var constant = ChangeTypeToExpression(item, exp.Body.Type);
            //以判断符或方法连接
            return QueryMethodExpression.Dictionary[item.Method](exp.Body, constant);
        }

       static private LambdaExpression GetPropertyLambdaExpression(ConditionItem item, ParameterExpression param)
        {
            //获取每级属性如c.Users.Proiles.UserId
            var props = item.Field.Split('.');
            Expression propertyAccess = param;
            var typeOfProp = typeof(T);
            int i = 0;
            do
            {
                PropertyInfo property = typeOfProp.GetProperty(props[i]);
                if (property == null) return null;
                typeOfProp = property.PropertyType;
                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                i++;
            } while (i < props.Length);

            return Expression.Lambda(propertyAccess, param);
        }

        #region ChangeType

        /// <summary>
        /// 类型转换，支持非空类型与可空类型之间的转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object ChangeType(object value, Type conversionType)
        {
            if (value == null) return null;
            return Convert.ChangeType(value, TypeUtil.GetUnNullableType(conversionType), CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 转换SearchItem中的Value的类型，为表达式树
        /// </summary>
        /// <param name="item"></param>
        /// <param name="conversionType">目标类型</param>
        public static Expression ChangeTypeToExpression(ConditionItem item, Type conversionType)
        {
            if (item.Value == null) return Expression.Constant(item.Value, conversionType);
            #region 数组
            if (item.Method == QueryMethod.StdIn)
            {
                var arr = (item.Value as Array);
                var expList = new List<Expression>();
                //确保可用
                if (arr != null)
                {
                    var collection =
                        arr.Cast<object>()
                            .Select((t, i) => ChangeType(arr.GetValue(i), conversionType))
                            .Select(c => Expression.Constant(c, conversionType));
                    expList.AddRange(collection);
                }
                //构造inType类型的数组表达式树，并为数组赋初值
                return Expression.NewArrayInit(conversionType, expList);
            }

            #endregion

            var elementType = TypeUtil.GetUnNullableType(conversionType);
            //var value = Convert.ChangeType(item.Value, elementType, CultureInfo.CurrentCulture);
            var value = TypeUtil.ChangeType(item.Value, elementType);

            return Expression.Constant(value, conversionType);
        }

        #endregion

    }
}