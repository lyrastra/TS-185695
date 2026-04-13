using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Moedelo.Finances.WebApp.Infrastructure.Exceptions;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.Finances.WebApp.Infrastructure.WebApi
{
    public class ValidateModelFilterAttribute : ActionFilterAttribute
    {
        private ILogger logger;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            logger = logger ?? (ILogger)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogger));
            if (actionContext.ModelState.IsValid == false)
            {
                logger.Error(nameof(ValidateModelFilterAttribute), actionContext.ModelState.ToJsonString());
                throw new ModelValidationException();
            }
        }
    }
}