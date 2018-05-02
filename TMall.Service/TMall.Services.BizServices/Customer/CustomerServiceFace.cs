using TMall.DomainModels.Customer;
using TMall.Services.IBizServices.Customer;

namespace TMall.Services.BizServices.Customer
{
    public class CustomerServiceFace
    {
        private readonly ICustomerBizService _customerBizService;

        public CustomerServiceFace(ICustomerBizService customerBizService)
        {
            _customerBizService = customerBizService;
        }

        #region QuerySerivce

        /// <summary>
        /// 检测用户是否存在
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsExistCustomer(string customerName, string password)
        {
            return _customerBizService.IsExistCustomer(customerName, password);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public CustomerInfo GetCustomer(string customerName, string password)
        {
            return _customerBizService.GetCustomer(customerName, password);
        }

        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        public bool IsExistCustomerName(string customerName)
        {
            return _customerBizService.IsExistCustomerName(customerName);
        }

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        public bool AddCustomer(CustomerInfo customerInfo)
        {
            return _customerBizService.AddCustomer(customerInfo);
        }

        #endregion

    }
}
