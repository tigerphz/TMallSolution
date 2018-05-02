using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using System.Web.Mvc;
using TMall.Infrastructure.Utility;

namespace TMall.ManageSite.ViewModel.FluentValidate
{
    public class ModelValidatorFactory : IValidatorFactory
    {
        public IValidator GetValidator(Type type)
        {
            Check.NotNull(type, "type");
            return DependencyResolver.Current.GetService(typeof(IValidator<>).MakeGenericType(type)) as IValidator;
        }

        public IValidator<T> GetValidator<T>()
        {
            return DependencyResolver.Current.GetService<IValidator<T>>();
        }
    }
}
