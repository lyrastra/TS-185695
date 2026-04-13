using System;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Business.Services.Integrations.Exceptions;
using Moedelo.Finances.Domain.Interfaces.Business.Integrations;
using Moedelo.Finances.WebApp.ClientData.Integrations;
using Moedelo.Finances.WebApp.Mappers.Integrations;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.Finances.WebApp.Controllers
{
    [RoutePrefix("Integrations/Statements")]
    public class IntegrationsStatementsController : BaseApiController
    {
        private const string TAG = nameof(IntegrationsStatementsController);

        private readonly ILogger logger;
        private readonly IUserContext userContext;
        private readonly IStatementRequestService statementRequestService;

        public IntegrationsStatementsController(
            ILogger logger,
            IUserContext userContext,
            IStatementRequestService statementRequestService)
        {
            this.logger = logger;
            this.userContext = userContext;
            this.statementRequestService = statementRequestService;
        }

        [HttpPost]
        [Route("RequestByIntegrationPartner")]
        public async Task<IHttpActionResult> SendRequestByIntegrationPartnerAsync(StatementRequestByIntegrationPartnerClientData clientData)
        {
            try
            {
                var request = StatementRequestMapper.MapBankStatementRequest(clientData);
                var response = await statementRequestService.SendStatementRequestAsync(userContext, request).ConfigureAwait(false);
                return Json(StatementRequestMapper.MapBankStatementResponse(response));
            }
            catch (SettlementAccountNotFoundException)
            {
                return Json(new ErrorStatementResponseClientData("Рaсчетный счет для данной интеграции не найден"));
            }
            catch (Exception ex)
            {
                logger.Error(TAG, ex.Message, ex);
                return Json(new ErrorStatementResponseClientData("Произошла техническая ошибка"));
            }
        }
    }
}