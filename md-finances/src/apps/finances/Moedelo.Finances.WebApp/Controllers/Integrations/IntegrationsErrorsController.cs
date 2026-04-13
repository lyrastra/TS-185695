using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Integrations;
using Moedelo.Finances.WebApp.ClientData.Integrations;
using Moedelo.Finances.WebApp.Mappers.Integrations;

namespace Moedelo.Finances.WebApp.Controllers
{
    [RoutePrefix("Integrations/Errors")]
    public class IntegrationsErrorsController : BaseApiController
    {
        private readonly IUserContext userContext;
        private readonly IIntegrationErrorsService integrationService;

        public IntegrationsErrorsController(
            IUserContext userContext,
            IIntegrationErrorsService errorsService)
        {
            this.userContext = userContext;
            this.integrationService = errorsService;
        }

        [HttpGet]
        [Route("")]
        [Route("~/Integrations/GetIntegrationErrors")] // old
        public async Task<List<IntegrationErrorClientData>> GetIntegrationErrorsAsync(CancellationToken ctx)
        {
            var errors = await integrationService
                .GetIntegrationErrorsAsync(userContext.FirmId, ctx)
                .ConfigureAwait(false);

            return errors.Select(x => x.Map()).ToList();
        }

        [HttpPost]
        [Route("SetAsRead")]
        [Route("~/Integrations/SetIntegrationErrorAsRead")] // old
        public async Task<HttpResponseMessage> SetIntegrationErrorAsReadAsync(IReadOnlyCollection<int> errorIds)
        {
            await integrationService.SetIntegrationErrorAsReadAsync(userContext.FirmId, errorIds).ConfigureAwait(false);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}