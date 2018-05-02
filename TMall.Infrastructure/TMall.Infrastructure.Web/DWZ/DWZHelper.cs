using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TMall.Infrastructure.Data;
using TMall.Framework.ExceptionHanding;

namespace TMall.Infrastructure.Web
{
    /// <summary>
    /// DWZ UI 框架帮助类
    /// </summary>
    public static class DWZHelper
    {
        /// <summary>
        /// 成功状态
        /// </summary>
        private const string DWZOK = "200";

        /// <summary>
        /// 错误状态
        /// </summary>
        private const string DWZERROR = "300";

        /// <summary>
        /// 超时状态
        /// </summary>
        private const string DWZTIMEOUT = "301";

        private const string CLOSECURRENT = "closeCurrent";
        private const string FORWARD = "forward";

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        public static JsonResult ReturnSucc(string message)
        {
            return ReturnWithStatusCode(message, DWZOK);
        }

        public static JsonResult ReturnSuccAndClose(string message)
        {
            return ReturnWithStatusCode(message, DWZOK, CLOSECURRENT);
        }

        /// <summary>
        /// 返回失败
        /// </summary>
        /// <returns></returns>
        public static JsonResult ReturnError(string message)
        {
            return ReturnWithStatusCode(message, DWZERROR);
        }

        /// <summary>
        /// 返回失败并关闭页面
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static JsonResult ReturnErrorAndClose(string message)
        {
            return ReturnWithStatusCode(message, DWZERROR, CLOSECURRENT);
        }

        /// <summary>
        /// 返回超时
        /// </summary>
        /// <returns></returns>
        public static JsonResult ReturnTimeout(string message)
        {
            return ReturnWithStatusCode(message, DWZTIMEOUT);
        }

        private static JsonResult ReturnWithStatusCode(string message, string statusCode, string callbackType = "")
        {
            return new JsonResult()
            {
                Data = new DWZReturn()
                {
                    statusCode = statusCode,
                    message = message,
                    callbackType = callbackType
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        ///  关闭新窗体或者对话框，并刷新父页面
        /// </summary>
        /// <returns></returns>
        public static JsonResult ReturnRefrash(string navTabId, string message, string callbackType = CLOSECURRENT)
        {
            return new JsonResult()
            {
                Data = new DWZReturn()
                           {
                               statusCode = DWZOK,
                               message = message,
                               navTabId = navTabId,
                               callbackType = callbackType
                           },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <param name="navTabId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static JsonResult ReturnOnlyRefrash(string navTabId, string message)
        {
            return ReturnRefrash(navTabId, message, "");
        }

        /// <summary>
        /// 成功并关闭窗体或对话框
        /// </summary>
        /// <returns></returns>
        public static JsonResult ReturnForward(string forwardUrl, string message)
        {
            return new JsonResult()
            {
                Data = new DWZReturn()
                {
                    statusCode = DWZOK,
                    message = message,
                    callbackType = FORWARD,
                    forwardUrl = forwardUrl
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// DWZ操作返回行为扩展方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="navId"></param>
        /// <param name="message"></param>
        /// <param name="status"></param>
        /// <param name="returnDWZType"></param>
        /// <param name="forwardUrl"></param>
        /// <returns></returns>
        public static JsonResult ReturnDWZOperate(this Controller source, string navId, string message, bool status, ReturnDWZType returnDWZType, string forwardUrl = "")
        {
            if (!status)
            {
                return ReturnError(message);
            }

            switch (returnDWZType)
            {
                case ReturnDWZType.ReturnRefrash:
                    return ReturnRefrash(navId, message);
                case ReturnDWZType.ReturnOnlyRefrash:
                    return ReturnOnlyRefrash(navId, message);
                case ReturnDWZType.ReturnForward:
                    return ReturnForward(forwardUrl, message);
                case ReturnDWZType.ReturnSucc:
                    return ReturnSucc(message);
                case ReturnDWZType.ReturnSuccAndClose:
                    return ReturnSuccAndClose(message);
                default:
                    throw ExceptionHelper.ThrowComponentException("没有匹配到 ReturnDWZType 枚举类型");
            }
        }

        /// <summary>
        /// DWZ操作返回错误信息扩展方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static JsonResult ReturnDWZError(this Controller source, string message)
        {
            return ReturnError(message);
        }

        /// <summary>
        /// 拼接[
        ///           ["all", "所有城市"],
        ///           ["bj", "北京市"]
        ///     ]格式的json字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToDWZDDLJson(this List<SelectListItem> source)
        {
            List<string> itemList = new List<string>();
            if (source != null && source.Count() > 0)
            {
                source.ToList().ForEach(x =>
                {
                    itemList.Add(string.Format("[\"{0}\", \"{1}\"]", x.Value, x.Text));
                });

                return "[" + string.Join(",", itemList.ToArray()) + "]";
            }

            return string.Empty;
        }
    }
}
