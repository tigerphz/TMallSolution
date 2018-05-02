using System.Linq;
using TMall.DomainModels.Customer;
using TMall.Infrastructure.Data;
using System.Collections.Generic;

namespace TMall.Services.IRepository.Customer
{
    public interface ICustomerRepository : IRepository<CustomerInfo>
    {
        #region QuerySerivce

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="sysNo"></param>
        /// <returns></returns>
        CustomerInfo GetCustomerById(string sysNo);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        CustomerInfo GetCustomerByCustomerName(string customerName);

        /// <summary>
        /// 获取PasswordSalt
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        string GetPasswordSalt(string customerName);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        CustomerInfo GetCustomer(string customerName, string passwordHash);

        /// <summary>
        /// 用户是否已存在
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool IsExistCustomer(string customerName, string passwordHash);

        /// <summary>
        /// 通过名字查找用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IList<CustomerInfo> FindByName(string name);

        /// <summary>
        /// 用户名是否已存在
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
        void AddCustomer(CustomerInfo customerInfo);

        #endregion

    }
}

