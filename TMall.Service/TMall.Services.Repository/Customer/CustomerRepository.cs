using System.Linq;
using TMall.Infrastructure.Utility;
using TMall.DomainModels.Customer;
using System.Data.Entity;
using System;
using TMall.Infrastructure.Data;
using TMall.Services.IRepository.Customer;
using System.Collections.Generic;

namespace TMall.Services.Repository.Customer
{
    public class CustomerRepository : EfRepositoryBase<CustomerInfo>, ICustomerRepository
    {
        public CustomerRepository(DbContext context)
            : base(context)
        {
            Check.NotNull(context, "value must not be null.");
        }

        #region QuerySerivce

        public CustomerInfo GetCustomerById(string sysNo)
        {
            return this.GetByKey(sysNo);
        }

        public CustomerInfo GetCustomerByCustomerName(string customerName)
        {
            return this.Entities.Where(x => x.CustomerName.Equals(customerName))
                       .FirstOrDefault();
        }

        /// <summary>
        /// 获取PasswordHash
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        public string GetPasswordSalt(string customerName)
        {
            return this.Entities.Where(x => x.CustomerName.Equals(customerName))
                .Select(x => x.PasswordSalt).FirstOrDefault();
        }

        public CustomerInfo GetCustomer(string customerName, string passwordHash)
        {
            return this.Entities.Where(x => x.CustomerName.Equals(customerName)
                && x.PasswordHash.Equals(passwordHash)).FirstOrDefault();
        }

        public bool IsExistCustomer(string customerName, string passwordHash)
        {
            int count = this.Entities.Count(x => x.CustomerName.Equals(customerName)
                                                 && x.PasswordHash.Equals(passwordHash));
            return count > 0;
        }

        public IList<CustomerInfo> FindByName(string name)
        {
            return this.Entities.Where(x => x.CustomerName.Equals(name)).ToList();
        }

        public bool IsExistCustomerName(string customerName)
        {
            int count = this.Entities.Count(x => x.CustomerName.Equals(customerName));
            return count > 0;
        }

        #endregion

        #region ActionSerivce

        public void AddCustomer(CustomerInfo customerInfo)
        {
            Insert(customerInfo);
        }

        #endregion

    }
}
