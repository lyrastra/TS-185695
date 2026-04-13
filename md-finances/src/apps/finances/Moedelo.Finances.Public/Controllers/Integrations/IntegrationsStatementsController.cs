using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Business.Services.Integrations.Exceptions;
using Moedelo.Finances.Domain.Interfaces.Business.Integrations;
using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.Finances.Public.ClientData.Integrations;
using Moedelo.Finances.Public.Mappers.Integrations;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;


namespace Moedelo.Finances.Public.Controllers
{
    [RoutePrefix("Integrations/Statements")]
    [ApiExplorerSettings(IgnoreApi = true)]
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
        [Route("Request")]
        public async Task<IHttpActionResult> RequestBySourcesAsync(StatementRequestClientData clientData)
        {
            logger.Info(TAG, $"Обновить из банка (Request)", userContext.GetAuditContext(), clientData);
            return Data(await SendBankStatementRequestsAsync(clientData).ConfigureAwait(false));
        }

        [HttpPost]
        [Route("RequestBySource")]
        public async Task<IHttpActionResult> RequestBySourceAsync(StatementRequestBySourceClientData clientData)
        {
            logger.Info(TAG, $"Обновить из банка за период (RequestBySource)", userContext.GetAuditContext(), clientData);
            switch (clientData.SourceType)
            {
                case MoneySourceType.All:
                    return Data(await SendBankStatementRequestsAsync(new StatementRequestClientData 
                    { StartDate = clientData.StartDate, 
                        EndDate = clientData .EndDate}
                    ).ConfigureAwait(false));
                case MoneySourceType.SettlementAccount:
                    return Data(await SendBankStatementRequestBySourceAsync(clientData).ConfigureAwait(false));
                case MoneySourceType.Purse:
                    return Data(await SendPurseRequestBySourceAsync(clientData).ConfigureAwait(false));
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }

        private async Task<StatementResponseBaseClientData> SendBankStatementRequestsAsync(StatementRequestClientData clientData)
        {
            try
            {
                var request = new BankStatementRequestBySettlementAccounts(clientData.StartDate, clientData.EndDate);
                var response = await statementRequestService.SendStatementRequestsAsync(userContext, request).ConfigureAwait(false);
                return StatementRequestMapper.MapBankStatementResponse(response);
            }
            catch (BankNotFoundException)
            {
                return new ErrorStatementResponseClientData("Банки не найдены");
            }
            catch (Exception ex)
            {
                logger.Error(TAG, ex.Message, ex);
                return new ErrorStatementResponseClientData("Произошла техническая ошибка");
            }
        }

        private async Task<StatementResponseBaseClientData> SendBankStatementRequestBySourceAsync(StatementRequestBySourceClientData clientData)
        {
            try
            {
                var request = StatementRequestMapper.MapBankStatementRequest(clientData);
                var response = await statementRequestService.SendStatementRequestAsync(userContext, request).ConfigureAwait(false);
                return StatementRequestMapper.MapBankStatementResponse(response);
            }
            catch (SettlementAccountNotFoundException)
            {
                return new ErrorStatementResponseClientData("Рaсчетный счет не найден");
            }
            catch (BankNotFoundException)
            {
                return new ErrorStatementResponseClientData("Банк не найден");
            }
            catch (IntegrationNotFoundException)
            {
                return new ErrorStatementResponseClientData("Интеграция не подключена");
            }
            catch (Exception ex)
            {
                logger.Error(TAG, ex.Message, ex);
                return new ErrorStatementResponseClientData("Произошла техническая ошибка");
            }
        }

        private async Task<StatementResponseBaseClientData> SendPurseRequestBySourceAsync(StatementRequestBySourceClientData clientData)
        {
            try
            {
                var request = PurseRequestMapper.MapPurseRequest(clientData);
                var response = await statementRequestService.SendPurseStatementRequestAsync(userContext, request).ConfigureAwait(false);
                return PurseRequestMapper.MapPurseResponse(response);
            }
            catch (KontragentNotFoundException ex)
            {
                return new ErrorStatementResponseClientData(ex.Message);
            }
            catch (PurseNotFoundException ex)
            {
                return new ErrorStatementResponseClientData(ex.Message);
            }
            catch (IntegrationNotFoundException ex)
            {
                return new ErrorStatementResponseClientData(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(TAG, ex.Message, ex);
                return new ErrorStatementResponseClientData("Произошла техническая ошибка");
            }
        }
    }
}