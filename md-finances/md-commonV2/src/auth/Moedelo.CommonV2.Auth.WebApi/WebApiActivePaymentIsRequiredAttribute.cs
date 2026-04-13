using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Moedelo.CommonV2.UserContext.Domain;

namespace Moedelo.CommonV2.Auth.WebApi;

public class WebApiActivePaymentIsRequiredAttribute : ActionFilterAttribute
{
    public bool AllowTrial { get; set; } = false;

    public override async Task OnActionExecutingAsync(HttpActionContext actionContext,
        CancellationToken cancellationToken)
    {
        if (GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserContext)) is IUserContext
            userContext)
        {
            var billingInfo = await userContext.GetBillingContextDataAsync().ConfigureAwait(true);

            if (billingInfo.IsPaid)
            {
                return;
            }

            if (billingInfo.IsTrial && AllowTrial)
            {
                return;
            }
        }
        
        actionContext.Response =
            actionContext.Request.CreateResponse(HttpStatusCode.PaymentRequired, "No one active payment found");
    }
}
