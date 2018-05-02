using System;
using TMall.WebSite.IBizProcess;
using TMall.WebSite.ViewModel;
using TMall.WebSite.ViewModel.Mapping;
using TMall.DomainModels.Customer;
using TMall.Framework.ServiceLocation;
using TMall.Services.BizServices.Customer;
using TMall.Infrastructure.Core;

namespace TMall.WebSite.BizProcess
{
    public class AccountBizProcess : IAccountBizProcess
    {
        private readonly CustomerServiceFace _customerServiceFace;

        public AccountBizProcess(CustomerServiceFace customerServiceFace)
        {
            _customerServiceFace = customerServiceFace;
        }

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        public bool CreateCustomer(CustomerVM customerModel)
        {
            CustomerInfo customer = customerModel.ToModel();
            customer.RegisterTime = customer.LastLoginTime = customer.UpdateTime = DateTime.Now;
            
            return _customerServiceFace.AddCustomer(customer);
        }

        /// <summary>
        /// 检测用户是否存在
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public CustomerVM Login(string customerName, string password)
        {
            CustomerInfo customerInfo = _customerServiceFace.GetCustomer(customerName, password);
            return customerInfo.ToVM();
        }
         
        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsExistCustomer(string customerName, string password)
        {
            return _customerServiceFace.IsExistCustomer(customerName, password);
        } 
    }
}
