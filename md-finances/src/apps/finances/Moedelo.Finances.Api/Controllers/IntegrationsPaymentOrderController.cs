using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.CommonV2.Auth.WebApi;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Api.Mappers;
using Moedelo.Finances.Domain.Interfaces.Business.Integrations;

namespace Moedelo.Finances.Api.Controllers
{
    [RoutePrefix("Integrations/PaymentOrder")]
    [WebApiRejectUnauthorizedRequest]
    public class IntegrationsPaymentOrderController : ApiController
    {
        private readonly IUserContext userContext;
        private readonly IIntegrationPaymentOrderSender paymentOrderSender;

        public IntegrationsPaymentOrderController(
            IUserContext userContext,
            IIntegrationPaymentOrderSender paymentOrderSender)
        {
            this.userContext = userContext;
            this.paymentOrderSender = paymentOrderSender;
        }

        [HttpPost]
        [Route("Send")]
        public async Task<IHttpActionResult> SendAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var response = await paymentOrderSender.SendAsync(userContext, documentBaseIds);

            return Ok(response.Map());
        }
    }
}