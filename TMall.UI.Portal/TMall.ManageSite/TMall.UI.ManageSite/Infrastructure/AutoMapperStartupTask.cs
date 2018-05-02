using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMall.Infrastructure.Core;
using TMall.ManageSite.ViewModel.Mapping;

namespace TMall.UI.ManageSite.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            AutoMapperCreateMap cm = new AutoMapperCreateMap();
            cm.CreateMap();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}