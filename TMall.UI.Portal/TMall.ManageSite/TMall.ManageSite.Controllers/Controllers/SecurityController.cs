using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TMall.Infrastructure.Utility;
using TMall.ManageSite.Controllers.Attributes;

namespace TMall.ManageSite.Controllers
{
    public class SecurityController : BaseController
    {
        public const string VALIDATECODE = "ValidateCode";

        [HttpGet]
        [Anonymous]
        public FileResult ValidateCode()
        {
            ValidateCodeHelper vCode = new ValidateCodeHelper();
            string code = vCode.CreateValidateCode(5);
            TempData[VALIDATECODE] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
    }
}
