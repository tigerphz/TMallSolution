using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TMall.Infrastructure.Data;
using TMall.Infrastructure.Utility;
using TMall.Infrastructure.SearchModel;

namespace TMall.Infrastructure.Web
{
    /// <summary>
    /// DropDownList下拉列表扩展帮助类
    /// </summary>
    public static class DDLValueExtensions
    {
        /// <summary>
        /// 将字典转换为List<SelectListItem> 供combox使用
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectListItems(this Dictionary<string, string> source)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (source == null)
            {
                return items;
            }

            source.Keys.ToList().ForEach(x =>
            {
                items.Add(new SelectListItem()
                {
                    Text = x,
                    Value = source[x]
                });
            });

            return items;
        }

        private static List<SelectListItem> AppendItemByType(this List<SelectListItem> source, AppendItemType appendItemType)
        {
            switch (appendItemType)
            {
                case AppendItemType.All:
                case AppendItemType.Select:
                    source.Insert(0, new SelectListItem() { Selected = true, Value = "", Text = appendItemType.GetDescription() });
                    break;
            }

            return source;
        }

        /// <summary>
        /// Enum转换为List<SelectListItem>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appendItemType"></param>
        /// <returns></returns>
        public static List<SelectListItem> EnumToSelectListItems(Type enumType, AppendItemType appendItemType)
        {
            Dictionary<string, string> descValue = EnumHelper.GetEnumDescValue(enumType);
            List<SelectListItem> listItem = descValue.ToSelectListItems();

            return listItem.AppendItemByType(appendItemType);
        }

        /// <summary>
        /// Enum转换为List<SelectListItem>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appendItemType"></param>
        /// <returns></returns>
        public static List<SelectListItem> EnumToSelectListItems<T>(AppendItemType appendItemType)
        {
            return EnumToSelectListItems(typeof(T), appendItemType);
        }

        /// <summary>
        /// IList<T>转为为List<SelectListItem>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="textFun"></param>
        /// <param name="valueFun"></param>
        /// <param name="appendItemType"></param>
        /// <returns></returns>
        public static List<SelectListItem> ListToSelectListItems<T>(this IList<T> source, Func<T, string> textFun, Func<T, string> valueFun, AppendItemType appendItemType) where T : class
        {
            List<SelectListItem> listItem = new List<SelectListItem>();
            foreach (T t in source)
            {
                listItem.Add(new SelectListItem()
                {
                    Text = textFun(t),
                    Value = valueFun(t)
                });
            }

            return listItem.AppendItemByType(appendItemType);
        }
    }
}
