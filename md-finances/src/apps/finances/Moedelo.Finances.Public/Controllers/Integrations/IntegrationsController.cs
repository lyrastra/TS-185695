using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Integrations;
using Moedelo.Finances.Public.ClientData.Integrations;
using Moedelo.Finances.Public.Mappers.Integrations;

namespace Moedelo.Finances.Public.Controllers
{
    [RoutePrefix("Integrations")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IntegrationsController : BaseApiController
    {
        private readonly IUserContext userContext;
        private readonly IIntegrationPaymentOrderSender paymentOrderSender;

        public IntegrationsController(
            IUserContext userContext,
            IIntegrationPaymentOrderSender paymentOrderSender)
        {
            this.userContext = userContext;
            this.paymentOrderSender = paymentOrderSender;
        }

        [HttpPost]
        [Route("SendPaymentOrders")]
        public async Task<IHttpActionResult> SendPaymentOrdersAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var response = await paymentOrderSender.SendAsync(userContext, documentBaseIds).ConfigureAwait(false);
            return Data(IntegrationsMapper.MapSendPaymentOrdersResponseToClient(response));
        }
        
        [HttpPost]
        [Route("SendBankInvoice")]
        public async Task<IHttpActionResult> SendBankInvoiceAsync(SendBankInvoiceRequestClientData clientData)
        {
            var request = clientData.Map();
            var response = await paymentOrderSender.SendBankInvoiceAsync(userContext, request).ConfigureAwait(false);
            return Data(response);
        }
    }
}