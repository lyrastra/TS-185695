using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Moedelo.CommonV2.UserContext.Domain;

namespace Moedelo.CommonV2.Auth.WebApi;

public class WebApiRoleConstraintAttribute : ActionFilterAttribute
{
    private string[] PermittedRoleCodes { get; }

    public WebApiRoleConstraintAttribute(string[] values)
    {
        PermittedRoleCodes = values.ToArray();
    }

    public override async Task OnActionExecutingAsync(HttpActionContext actionContext,
        CancellationToken cancellationToken)
    {
        if (GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserContext)) is IUserContext
            userContext)
        {
            var extraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(true);

            if (!PermittedRoleCodes.Contains(extraData.RoleCode))
            {
                actionContext.Response =
                    actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Role is not permitted");
            }
        }

        await base.OnActionExecutingAsync(actionContext, cancellationToken).ConfigureAwait(true);
    }
}
