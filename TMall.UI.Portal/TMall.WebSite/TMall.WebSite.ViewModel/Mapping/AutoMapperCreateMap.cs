using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using TMall.DomainModels.Management;
using TMall.DomainModels.Customer;

namespace TMall.WebSite.ViewModel.Mapping
{
    /// <summary>
    /// 初始化所有需要映射的类的映射关系
    /// </summary>
    public class AutoMapperCreateMap
    {
        /// <summary>
        /// 初始化映射
        /// </summary>
        public void CreateMap()
        {
            #region 用户模块

            Mapper.CreateMap<CustomerInfo, CustomerVM>();
            Mapper.CreateMap<CustomerVM, CustomerInfo>();

            #endregion

            #region

            #endregion
        }
    }
}
