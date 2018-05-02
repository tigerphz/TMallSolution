using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;
using System;
namespace TMall.Infrastructure.Utility
{
    public static class EnumHelper
    {
        /// <summary>
        /// 从枚举类型和它的特性读出并返回一个键值对
        /// </summary>
        /// <returns>键值对</returns>
        public static Dictionary<string, string> GetEnumDescValue<T>()
        {
            return GetEnumDescValue(typeof(T));
        }

        /// <summary>
        /// /// <summary>
        /// 从枚举类型和它的特性读出并返回一个键值对
        /// </summary>
        /// <param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>
        /// <returns>键值对</returns>
        public static Dictionary<string, string> GetEnumDescValue(Type enumType)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            Type typeDescription = typeof(DescriptionAttribute);            
            System.Reflection.FieldInfo[] fields = enumType.GetFields();
            string strText = string.Empty;
            string strValue = string.Empty;
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    strValue = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        DescriptionAttribute aa = (DescriptionAttribute)arr[0];
                        strText = aa.Description;
                    }
                    else
                    {
                        strText = field.Name;
                    }
                    keyValues.Add(strText, strValue);
                }
            }
            return keyValues;
        }

        /// <summary>
        /// 获取Enum的描述信息
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            Type enumType = enumValue.GetType();
            while (((enumType.IsGenericType && (enumType.GetGenericTypeDefinition() == typeof(Nullable<>))) && ((enumType.GetGenericArguments() != null) && (enumType.GetGenericArguments().Length == 1))) && enumType.GetGenericArguments()[0].IsEnum)
            {
                enumType = enumType.GetGenericArguments()[0];
            }
            if (!enumType.IsEnum)
            {
                return string.Empty;
            }
            string name = enumValue.ToString();
            FieldInfo field = enumType.GetField(name);
            if (field == null)
            {
                return name;
            }
            object[] customAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if ((customAttributes != null) && (customAttributes.Length > 0))
            {
                DescriptionAttribute attribute = customAttributes[0] as DescriptionAttribute;
                name = attribute.Description;
            }

            return (name ?? field.Name);
        }
    }
}