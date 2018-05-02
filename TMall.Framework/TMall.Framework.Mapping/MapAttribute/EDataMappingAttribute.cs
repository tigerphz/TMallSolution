using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TMall.Framework.Mapping.MapAttribute
{
    /// <summary>
    /// 为映射成员设置别名
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class EDataMappingAttribute : Attribute
    {
        public EDataMappingAttribute() { }

        public EDataMappingAttribute(string name, DbType type)
        {
            MapName = name;
            MapType = type;
        }

        public string MapName { get; set; }

        public DbType MapType { get; set; }
    }
}
