using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmitMapper;
using TMall.Framework.Mapping.Configuration;
using EmitMapper.MappingConfiguration;

namespace TMall.Framework.Mapping
{
    public class MapperHelper
    {
        public static TTo DynamicToEntity<TTo>(dynamic valueObj)
        {
            var mapper =
                ObjectMapperManager.DefaultInstance
                .GetMapper<dynamic, TTo>(new AnonymousClassMapingConfig());

            return mapper.Map(valueObj);
        }

        /// <summary>
        /// 映射集合
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static TTo MapList<TFrom, TTo>(TFrom from)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(
                 new DefaultMapConfig().ConvertGeneric(typeof(IList<>), typeof(IList<>),
                 new DefaultCustomConverterProvider(typeof(EntityListToModelListConverter<,>))));

            return mapper.Map(from);
        }
    }
}
