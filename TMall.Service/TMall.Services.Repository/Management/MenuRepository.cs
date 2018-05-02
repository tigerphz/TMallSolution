using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using System.Data.Entity;
using TMall.Infrastructure.Utility;
using TMall.DomainModule.Enums;
using TMall.Infrastructure.Data;
using TMall.Services.IRepository.Management;

namespace TMall.Services.Repository.Management
{
    public class MenuRepository : EfRepositoryBase<MenuInfo>, IMenuRepository
    {
        public MenuRepository(DbContext context)
            : base(context)
        {
            Check.NotNull(context, "value must not be null.");
        }

        public void DeleteMenu(MenuInfo menuInfo)
        {
            var childMenus = this.Entities.Where(x => x.ParentNo == menuInfo.SysNo)
                                .Select(x => x.SysNo).ToList();

            MenuInfo module;
            if (childMenus != null && childMenus.Count > 0)
            {
                childMenus.ForEach(x =>
                {
                    module = new MenuInfo() { SysNo = x };
                    Delete(module);
                });
            }

            Delete(menuInfo);
        }
    }
}
