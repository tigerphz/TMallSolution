using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.Infrastructure.Utility;
using TMall.DomainModels.Customer;

namespace TMall.WebSite.ViewModel.Mapping
{
    public static class CustomerMapping
    {
        public static CustomerInfo ToModel(this CustomerVM customerModel)
        {
            return AutoMapper.Mapper.Map<CustomerInfo>(customerModel);
        }

        public static CustomerVM ToVM(this CustomerInfo customerInfo)
        {
            return AutoMapper.Mapper.Map<CustomerVM>(customerInfo);
        }
    }
}
