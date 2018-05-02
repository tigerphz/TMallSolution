using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace TMall.ManageSite.ViewModel.FluentValidators
{
    public class LoginVMValidator : AbstractValidator<LoginVM>
    {
        public LoginVMValidator()
        {
            RuleFor(x => x.SysUserName).NotEmpty().WithMessage("请输入用户名").WithName("用户名");
            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("请输入密码").WithName("密码");
        }
    }
}
