using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TMall.Framework.ExceptionHanding
{
    /// <summary>
    /// 业务访问层异常类，用于封装业务逻辑层引发的异常供表现层抓取
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        public BusinessException() : base() { }

        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, Exception inner) : base(message, inner) { }

        public BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
