using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TMall.Infrastructure.Data;
using TMall.Infrastructure.SearchModel;

namespace TMall.Infrastructure.Web
{
    public static class ViewDataHelper
    {
        /// <summary>
        /// 设置分页信息到viewData供页面使用
        /// </summary>
        /// <param name="viewData"></param>
        /// <param name="pageInfo"></param>
        public static void SetPageInfo(this ViewDataDictionary viewData, PageInfo pageInfo)
        {
            viewData["TotalCount"] = pageInfo.TotalCount;
            viewData["PageIndex"] = pageInfo.PageIndex;
            viewData["PageSize"] = pageInfo.PageSize;
            viewData["OrderField"] = pageInfo.OrderField;
            viewData["OrderDirection"] = pageInfo.OrderDirection.ToString();
        }

        /// <summary>
        /// 重新绑定查询信息到界面
        /// </summary>
        /// <param name="viewData"></param>
        /// <param name="searchCondition"></param>
        public static void ReBindSearchData(this ViewDataDictionary viewData, SearchCondition searchCondition)
        {
            //重新绑定查询信息到界面
            if (searchCondition != null && searchCondition.Items != null && searchCondition.Items.Count > 0)
            {
                foreach (var item in searchCondition.Items)
                {
                    viewData.Add(item.Field, item.Value);
                }
            }
        }

        public static void BindErrorMessage(this ViewDataDictionary viewData, ModelStateDictionary modelState)
        {
            List<string> errorMsg = new List<string>();
            foreach (var value in modelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                        errorMsg.Add(error.ErrorMessage);
                }
            }

            viewData["ErrorMsg"] = errorMsg;
        }
    }
}
