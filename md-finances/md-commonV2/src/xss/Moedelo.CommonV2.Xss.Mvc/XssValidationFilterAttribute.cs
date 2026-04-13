using System;
using System.Web.Mvc;

namespace Moedelo.CommonV2.Xss.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class XssValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            foreach (var parameter in filterContext.ActionParameters)
            {
                XssValidator.Validate(parameter.Value);
            }
        }
    }
}