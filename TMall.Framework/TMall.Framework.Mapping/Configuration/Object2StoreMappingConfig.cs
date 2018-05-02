using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmitMapper.MappingConfiguration;
using EmitMapper.MappingConfiguration.MappingOperations;
using EmitMapper.Utils;

namespace TMall.Framework.Mapping.Configuration
{
    public class Object2StoreMappingConfig : DefaultMapConfig
    {
        public override IMappingOperation[] GetMappingOperations(Type from, Type to)
        {
            return
                FilterOperations(
                    from,
                    to,
                    ReflectionUtils
                    .GetPublicFieldsAndProperties(from)
                    .Select(
                        m =>
                        (IMappingOperation)new SrcReadOperation
                        {
                            Source = new MemberDescriptor(m),
                            Setter = (obj, value, state) =>
                                ((IDictionary<string, object>)obj)[m.Name] = value
                        }
                    )
                ).ToArray();
        }
    }
}
