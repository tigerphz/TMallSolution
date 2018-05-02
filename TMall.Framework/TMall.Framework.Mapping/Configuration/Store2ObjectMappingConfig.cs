using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmitMapper.MappingConfiguration;
using EmitMapper.MappingConfiguration.MappingOperations;
using EmitMapper.Utils;

namespace TMall.Framework.Mapping.Configuration
{
    public class Store2ObjectMappingConfig : DefaultMapConfig
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
                                    (value, state) => ValueToWrite<object>.ReturnValue(
                                        ((IDictionary<string, object>)value)[m.Name]
                                    )
                                )

                        }
                    )
                ).ToArray();
        }
    }
}
