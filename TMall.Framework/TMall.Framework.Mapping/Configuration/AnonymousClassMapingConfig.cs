using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmitMapper.MappingConfiguration;
using EmitMapper.MappingConfiguration.MappingOperations;
using EmitMapper.Utils;
using System.Reflection;

namespace TMall.Framework.Mapping.Configuration
{
    public class AnonymousClassMapingConfig : DefaultMapConfig
    {
        public override IMappingOperation[] GetMappingOperations(Type from, Type to)
        {
            return
                FilterOperations(
                    from,
                    to,
                    ReflectionUtils
                    .GetPublicFieldsAndProperties(to)
                    .Select(
                        m =>
                        (IMappingOperation)new DestWriteOperation
                        {
                            Destination = new MemberDescriptor(m),
                            Getter =
                                (ValueGetter<object>)
                                (
                                    (value, state) => { return GetReturnValue(value, m); }
                                )
                        }
                    )
                ).ToArray();
        }

        private ValueToWrite<object> GetReturnValue(dynamic value, MemberInfo destMember)
        {
            Type type = value.GetType();
            List<MemberInfo> _sourceMembers = GetSourceMemebers(type);

            var matchedChain = GetMatchedChain(destMember.Name, _sourceMembers);

            if (matchedChain == null || matchedChain.Count == 0)
            {
                return ValueToWrite<object>.Skip();
            }
            if (matchedChain[0] is PropertyInfo)
            {
                var val = (matchedChain[0] as PropertyInfo).GetValue(value, null);
                return ValueToWrite<object>.ReturnValue(val);
            }
            else if (matchedChain[0] is FieldInfo)
            {
                var val = (matchedChain[0] as FieldInfo).GetValue(value);
                return ValueToWrite<object>.ReturnValue(val);
            }

            return ValueToWrite<object>.Skip();
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

        private List<MemberInfo> GetMatchedChain(string destName, List<MemberInfo> sourceMembers)
        {
            var matches = sourceMembers.Where(s => MatchMembers(destName, s.Name));
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
            if (!MatchMembers(destName, match.Name))
            {
                result.AddRange(
                    GetMatchedChain(destName.Substring(match.Name.Length), GetDestinationMemebers(match))
                );
            }
            return result;
        }
    }
}
