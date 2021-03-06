﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;

namespace TMall.Services.IRepository.Management
{
    public interface IMenuRepository : IRepository<MenuInfo>
    {
        void DeleteMenu(MenuInfo menuInfo);
    }
}
