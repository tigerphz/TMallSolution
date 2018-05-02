using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmitMapper;
using TMall.Framework.Mapping.Configuration;
using EmitMapper.MappingConfiguration;

namespace TMall.Framework.Mapping
{
    public class TypeMapping : ITypeMapping
    {
        public TTo MapTo<TFrom, TTo>(TFrom from)
        {
            ObjectsMapper<TFrom, TTo> mapper = ObjectMapperManager.DefaultInstance
                                         .GetMapper<TFrom, TTo>(new CommonMapingConfig());

            return mapper.Map(from);
        }
    }
}
