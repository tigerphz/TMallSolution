using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace TMall.Infrastructure.Utility
{
    public static class FunExtensions
    {
        /// <summary>
        /// 时间格式化
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Format(this DateTime source, string format = "yyyy-MM-dd hh:mm:ss")
        {
            return source.ToString(format);
        }

        /// <summary>
        /// bool转换为 是 否
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string IsYesOrNo(this bool source)
        {
            return source ? "是" : "否";
        }
    }
}
