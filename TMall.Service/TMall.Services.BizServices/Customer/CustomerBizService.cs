using System.Linq;
using TMall.Infrastructure.Utility;
using TMall.DomainModels.Customer;
using TMall.Framework.ServiceLocation;
using TMall.Infrastructure.Data;
using TMall.Services.IBizServices.Customer;
using TMall.Services.IRepository.Customer;
using System.Collections.Generic;

namespace TMall.Services.BizServices.Customer
{
    public class CustomerBizService : ICustomerBizService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerBizService(ICustomerRepository customerRepository)
        {
            Check.NotNull(customerRepository, "value must not be null.");

            _customerRepository = customerRepository;
            _unitOfWork = _customerRepository.UnitOfWork;
        }

        #region QuerySerivce

        public CustomerInfo GetCustomerById(string sysNo)
        {
            return _customerRepository.GetCustomerById(sysNo);
        }

        public CustomerInfo GetCustomerByCustomerName(string customerName)
        {
            return _customerRepository.GetCustomerByCustomerName(customerName);
        }

        public IList<CustomerInfo> FindByName(string name)
        {
            return _customerRepository.FindByName(name);
        }

        /// <summary>
        /// 检测用户是否存在
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsExistCustomer(string customerName, string password)
        {
            string passwordSalt = _customerRepository.GetPasswordSalt(customerName);
            if (string.IsNullOrEmpty(passwordSalt))
                return false;

            return _customerRepository.IsExistCustomer(customerName, passwordSalt);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public CustomerInfo GetCustomer(string customerName, string password)
        {
            string passwordSalt = _customerRepository.GetPasswordSalt(customerName);
            if (string.IsNullOrEmpty(passwordSalt))
                return null;

            string passwordHash = SecurityHelper.EncodePassword(password, passwordSalt);

            return _customerRepository.GetCustomer(customerName, passwordHash);
        }

        public bool IsExistCustomerName(string customerName)
        {
            return _customerRepository.IsExistCustomerName(customerName);
        }

        #endregion

        #region ActionService

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        public bool AddCustomer(CustomerInfo customerInfo)
        {
            //获取混淆码
            string passwordSalt = SecurityHelper.GenerateSalt();
            //获取混淆码加密过的密码
            string passwordHash = SecurityHelper.EncodePassword(customerInfo.PasswordHash, passwordSalt);
            customerInfo.PasswordHash = passwordHash;
            customerInfo.PasswordSalt = passwordSalt;

            _customerRepository.AddCustomer(customerInfo);

            _unitOfWork.Commit();
            return true;
        }

        #endregion

    }
}
