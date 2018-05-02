using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.Infrastructure.Web
{
    public enum ReturnDWZType
    {
        /// <summary>
        /// 返回成功
        /// </summary>
        ReturnSucc,

        /// <summary>
        /// 返回成功，并关闭窗体
        /// </summary>
        ReturnSuccAndClose,

        /// <summary>
        /// 关闭新窗体或者对话框，并刷新父页面
        /// </summary>
        ReturnRefrash,

        /// <summary>
        /// 刷新页面
        /// </summary>
        ReturnOnlyRefrash,

        /// <summary>
        /// 跳转到新页面
        /// </summary>
        ReturnForward
    }

    public class DWZReturn
    {
        /// <summary>
        /// 操作状态码
        /// </summary>
        public string statusCode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 需要刷新的navTabId(dialog或navtab)
        /// </summary>
        public string navTabId { get; set; }

        /// <summary>
        /// closeCurrent 关闭当前窗口
        /// </summary>
        public string callbackType { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        public string forwardUrl { get; set; }
    }
}
