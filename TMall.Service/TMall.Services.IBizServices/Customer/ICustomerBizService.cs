using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Customer;

namespace TMall.Services.IBizServices.Customer
{
    public interface ICustomerBizService
    {

        #region QuerySerivce

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        CustomerInfo GetCustomerByCustomerName(string customerName);

        /// <summary>
        /// 通过名次查找用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IList<CustomerInfo> FindByName(string name);

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool IsExistCustomer(string customerName, string password);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        CustomerInfo GetCustomer(string customerName, string password);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CustomerInfo GetCustomerById(string sysNo);

        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        bool IsExistCustomerName(string customerName);

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        bool AddCustomer(CustomerInfo customerInfo);

        #endregion

    }
}
