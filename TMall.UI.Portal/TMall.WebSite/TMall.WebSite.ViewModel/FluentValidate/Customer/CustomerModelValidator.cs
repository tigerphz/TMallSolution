using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace TMall.WebSite.ViewModel.FluentValidate
{
    public class CustomerModelValidator : AbstractValidator<CustomerVM>
    {
        public CustomerModelValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("请输入用户名").WithName("用户名");
            RuleFor(x => x.PasswordHash).NotEmpty().Length(6, 100).WithMessage("密码必须大于6位数").WithName("密码");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithName("电子邮箱");
            //RuleFor(x=>x.ConfirmPassword)
        }
    }
}
