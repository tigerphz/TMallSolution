using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using TMall.Framework.Mapping;
using AutoMapper;

namespace TMall.Infrastructure.Data
{
    public class UpdateInfo
    {
        public UpdateInfo() { }

        public dynamic dynamicData { get; set; }

        public string JsonData { get; set; }

        public T GetData<T>() where T : class
        {
            if (dynamicData != null)
            {
                return Mapper.DynamicMap<T>(dynamicData);
            }
            else if (!string.IsNullOrEmpty(JsonData))
            {
                return null;
            }

            throw new ArgumentNullException("需要更新的对象 dynamicData 或者 JsonData不能为空");
        }

        public string[] GetPropertyNames()
        {
            if (dynamicData != null)
            {
                return GetdynamicDataPropertyNames();
            }
            else if (!string.IsNullOrEmpty(JsonData))
            {
                return GetJsonDataPropertyNames();
            }

            throw new ArgumentNullException("需要更新的对象 dynamicData 或者 JsonData不能为空");
        }

        private string[] GetdynamicDataPropertyNames()
        {
            Type type = dynamicData.GetType();
            var propertyList = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (propertyList == null)
            {
                return null;
            }

            return propertyList.Select(x => x.Name).ToArray();
        }

        private string[] GetJsonDataPropertyNames()
        {
            return null;
        }
    }
}
