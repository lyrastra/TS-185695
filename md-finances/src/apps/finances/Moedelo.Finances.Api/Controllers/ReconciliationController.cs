using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Api.Mappers.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using Moedelo.Finances.Dto.Money;
using Moedelo.InfrastructureV2.Domain.Exceptions.ApiClient;
using Moedelo.Finances.Client.Money.Dto;

namespace Moedelo.Finances.Api.Controllers
{
    [RoutePrefix("Money/Reconciliation")]
    public class ReconciliationController : ApiController
    {
        private readonly IUserContext userContext;
        private readonly IReconciliationService reconciliationService;

        public ReconciliationController(
            IUserContext userContext,
            IReconciliationService reconciliationService)
        {
            this.userContext = userContext;
            this.reconciliationService = reconciliationService;
        }

        [HttpGet]
        [Route("Last")]
        public async Task<ReconciliationResponseDto> GetLastAsync(int settlementAccountId)
        {
            var result = await reconciliationService.GetLastInfoAsync(userContext.FirmId, settlementAccountId).ConfigureAwait(false);
            return result?.MapToDto();
        }

        [HttpPost]
        [Route("GetLastWithDiff")]
        public async Task<ReconciliationResponseDto[]> GetLastWithDiffAsync(LastReconciliationWithDiffRequestDto requestDto)
        {
            if (requestDto.SettlementAccountIds == null || requestDto.SettlementAccountIds.Any() == false)
            {
                throw new HttpRequestValidationException(HttpStatusCode.BadRequest, "Not valid SettlementAccountIds param");
            }

            var result = await reconciliationService.GetLastWithDiffInfoAsync(userContext.FirmId, requestDto.ReconciliationStatus, requestDto.SettlementAccountIds)
                .ConfigureAwait(false);
            return result.Select(x => x.MapToDto()).ToArray();
        }

        [HttpPost]
        [Route("GetStatuses")]
        public async Task<ReconciliationResponseDto[]> GetStatusesAsync([FromBody]ReconciliationStatusRequestDto requestDto)
        {
            if (requestDto.SettlementAccountIds == null || requestDto.SettlementAccountIds.Any() == false)
            {
                throw new HttpRequestValidationException(HttpStatusCode.BadRequest, "Not valid SettlementAccountIds param");
            }

            var reconciliationDate = requestDto.ReconciliationDate ?? DateTime.Now;
            var result = await reconciliationService.GetByDateAsync(userContext.FirmId, requestDto.SettlementAccountIds, reconciliationDate)
                .ConfigureAwait(false);

            return MoneyReconciliationMapper.MapToDto(result);
        }
    }
}
