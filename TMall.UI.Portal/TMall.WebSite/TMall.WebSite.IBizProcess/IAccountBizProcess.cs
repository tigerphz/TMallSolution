using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.WebSite.ViewModel;

namespace TMall.WebSite.IBizProcess
{
    public interface IAccountBizProcess
    {
        bool CreateCustomer(CustomerVM customerModel);

        bool IsExistCustomer(string customerName, string password);

        CustomerVM Login(string customerName, string password); 
    }
}
