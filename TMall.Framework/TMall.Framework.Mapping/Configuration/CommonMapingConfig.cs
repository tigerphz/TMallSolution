using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmitMapper.MappingConfiguration;
using System.Data;
using EmitMapper.MappingConfiguration.MappingOperations;
using EmitMapper;
using EmitMapper.Utils;
using System.Reflection;
using System.Diagnostics;
using TMall.Framework.Mapping.MapAttribute;

namespace TMall.Framework.Mapping.Configuration
{
    /// <summary>
    /// 自定义通用转换配置
    /// </summary>
    public class CommonMapingConfig : DefaultMapConfig
    {
        public override IMappingOperation[] GetMappingOperations(Type from, Type to)
        {
            //base.IgnoreMembers<object, object>(GetIgnoreFields(from).Concat(GetIgnoreFields(to)).ToArray());
            var ignoreMembers = GetIgnoreFields(from);

            return
            FilterOperations(
                from,
                to,
                ReflectionUtils
                .GetPublicFieldsAndProperties(from)
                .Except(ignoreMembers)
                .Select(
                    m =>
                    (IMappingOperation)new SrcReadOperation
                    {
                        Source = new MemberDescriptor(m),
                        Setter = (obj, value, state) =>
                            {
                                SetObjValue(obj, value, m);
                            }
                    }
                )
            ).ToArray();
        }

        List<MemberInfo> _DestinationMemebers;

        public void SetObjValue(object obj, object value, MemberInfo sourceMember)
        {
            if (_DestinationMemebers == null)
            {
                Type type = obj.GetType();
                _DestinationMemebers = GetDestinationMemebers(type);
            }

            string memberName = string.Empty;
            var customAttrs = sourceMember.GetCustomAttributes(typeof(EDataMappingAttribute), false);
            memberName = customAttrs != null && customAttrs.Length > 0 ? (customAttrs[0] as EDataMappingAttribute).MapName
                : sourceMember.Name;

            if (string.IsNullOrEmpty(memberName))
                return;

            List<MemberInfo> matchedChain = GetMatchedChain(memberName, _DestinationMemebers);

            if (matchedChain == null || matchedChain.Count == 0)
                return;

            if (matchedChain[0] is PropertyInfo)
            {
                (matchedChain[0] as PropertyInfo).SetValue(obj, value, null);
            }
            else if (matchedChain[0] is FieldInfo)
            {
                (matchedChain[0] as FieldInfo).SetValue(obj, value);
            }
        }


        /// <summary>
        /// 获取忽略的成员
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        //private IEnumerable<string> GetIgnoreFields(Type type)
        //{
        //    return type
        //        .GetFields()
        //        .Where(
        //            f => f.GetCustomAttributes(typeof(EIgnoreAttribute), false).Length > 0
        //        )
        //        .Select(f => f.Name)
        //        .Concat(
        //            type
        //            .GetProperties()
        //            .Where(
        //                p => p.GetCustomAttributes(typeof(EIgnoreAttribute), false).Length > 0
        //            )
        //            .Select(p => p.Name)
        //        );
        //}

        /// <summary>
        /// 获取忽略的成员
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private IEnumerable<MemberInfo> GetIgnoreFields(Type type)
        {
            return type
                .GetFields()
                .Where(
                    f => f.GetCustomAttributes(typeof(EIgnoreAttribute), false).Length > 0
                ).Select(f => f as MemberInfo)
                .Concat(
                    type
                    .GetProperties()
                    .Where(
                        p => p.GetCustomAttributes(typeof(EIgnoreAttribute), false).Length > 0
                    ).Select(f => f as MemberInfo)
                );
        }

        private static List<MemberInfo> GetSourceMemebers(Type t)
        {
            return GetMemebers(t)
                .Where(
                    m =>
                        m.MemberType == MemberTypes.Field ||
                        m.MemberType == MemberTypes.Property
                )
                .ToList();
        }

        private static List<MemberInfo> GetDestinationMemebers(MemberInfo mi)
        {
            Type t;
            if (mi.MemberType == MemberTypes.Field)
            {
                t = mi.DeclaringType.GetField(mi.Name).FieldType;
            }
            else
            {
                t = mi.DeclaringType.GetProperty(mi.Name).PropertyType;
            }
            return GetDestinationMemebers(t);
        }

        private static List<MemberInfo> GetDestinationMemebers(Type t)
        {
            return GetMemebers(t).Where(m => m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property).ToList();
        }

        private static List<MemberInfo> GetMemebers(Type t)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            return t.GetMembers(bindingFlags).ToList();
        }

        private List<MemberInfo> GetMatchedChain(string sourceName, List<MemberInfo> DestMembers)
        {
            var matches = DestMembers.Where(s =>
            {
                //return MatchMembers(sourceName, s.Name);
                return MatchMembers(sourceName, s);
            });

            int len = 0;
            MemberInfo match = null;
            foreach (var m in matches)
            {
                if (m.Name.Length > len)
                {
                    len = m.Name.Length;
                    match = m;
                }
            }
            if (match == null)
            {
                return null;
            }
            var result = new List<MemberInfo> { match };
            //if (!MatchMembers(sourceName, match.Name))
            if (!MatchMembers(sourceName, match))
            {
                result.AddRange(
                    GetMatchedChain(sourceName.Substring(match.Name.Length), GetDestinationMemebers(match))
                );
            }
            return result;
        }

        private bool MatchMembers(string sourceName, MemberInfo destMember)
        {
            string memberName = string.Empty;
            var customAttrs = destMember.GetCustomAttributes(typeof(EDataMappingAttribute), false);
            memberName = customAttrs != null && customAttrs.Length > 0 ? (customAttrs[0] as EDataMappingAttribute).MapName
                : destMember.Name;

            return MatchMembers(sourceName, memberName);
        }
    }
}