using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace TMall.ManageSite.ViewModel.FluentValidators
{
    public class MenuVMValidator : AbstractValidator<MenuVM>
    {
        public MenuVMValidator()
        {
            RuleFor(x => x.MenuName).NotEmpty().WithMessage("请输入主菜单名称").WithName("主菜单名称");
            RuleFor(x => x.Sort).NotEmpty().WithMessage("请输入菜单顺序号码").WithName("菜单顺序");
        }
    }
}