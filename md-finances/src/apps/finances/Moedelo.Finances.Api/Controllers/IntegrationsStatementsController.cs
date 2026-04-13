using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.CommonV2.Auth.WebApi;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Integrations;
using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.Finances.Dto.IntegrationStatements;

namespace Moedelo.Finances.Api.Controllers
{
    [RoutePrefix("Integrations/Statements")]
    [WebApiRejectUnauthorizedRequest]
    public class IntegrationsStatementsController : ApiController
    {
        private readonly IUserContext userContext;
        private readonly IStatementRequestService statementRequestService;

        public IntegrationsStatementsController(
            IUserContext userContext,
            IStatementRequestService statementRequestService)
        {
            this.userContext = userContext;
            this.statementRequestService = statementRequestService;
        }

        /// <summary>
        /// Запросить выписки по р/сч 
        /// </summary>
        /// <returns>Для каждого р/сч: статус запроса выписки или причина, почему запросить нельзя</returns>
        [HttpPost, Route("Request")]
        public async Task<IHttpActionResult> BankStatementRequest(StatementRequestDto requestDto)
        {
            var request = new BankStatementRequestBySettlementAccounts(requestDto.StartDate, requestDto.EndDate)
            {
                StopOnUnprocessedRequest = requestDto.StopOnUnprocessedRequest
            };
            var result = await statementRequestService.SendStatementRequestsVerboseAsync(
                userContext,
                request).ConfigureAwait(false);
            
            return Ok(result.Select(Map));
        }

        private static ResultOfStatementRequestDto Map(ResultOfStatementRequest model)
        {
            return new ResultOfStatementRequestDto
            {
                SettlementAccountId = model.SettlementAccountId,
                BlockedReason = model.BlockedReason,
                IsSuccess = model.IsSuccess,
                RequestId = model.RequestId,
                Error = model.Error
            };
        }
    }
}