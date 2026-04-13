using Moedelo.Docs.Dto;
using Moedelo.Docs.Dto.PurchaseInfo;
using Moedelo.Docs.Dto.Upd;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.Upd
{
    [InjectAsSingleton]
    public class UpdApiClient : BaseApiClient, IUpdApiClient
    {
        private readonly SettingValue apiEndpoint;

        public UpdApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("DocsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<UpdDto> GetByBaseIdAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<UpdDto>($"/Upd/GetByBaseId?firmId={firmId}&userId={userId}&baseId={baseId}");
        }

        public Task<List<TaxPostingDto>> GeneratePostingsByLinkedPaymentAsync(int firmId, int userId, LinkedPaymentDto dto)
        {
            return PostAsync<LinkedPaymentDto, List<TaxPostingDto>>(
                $"/Upd/GeneratePostingsByLinkedPayment?firmId={firmId}&userId={userId}",
                dto);
        }

        public Task<List<UpdWithItemsDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds,
            CancellationToken cancellationToken)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<UpdWithItemsDto>());
            }

            var uri = $"/Upd/GetByBaseIds?firmId={firmId}&userId={userId}";

            return PostAsync<IEnumerable<long>, List<UpdWithItemsDto>>(uri, baseIds, cancellationToken: cancellationToken);
        }

        public Task DeleteByBaseIdAsync(int firmId, int userId, long baseId)
        {
            return DeleteByBaseIdsAsync(firmId, userId, new[] { baseId });
        }

        public Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || baseIds.Count == 0)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/Upd/DeleteByBaseIds?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task<List<UpdWithItemsDto>> GetByPeriodAsync(int firmId, int userId, DateTime startDate,
            DateTime endDate, CancellationToken cancellationToken = default)
        {
            return GetAsync<List<UpdWithItemsDto>>("/Upd/GetByPeriod", new
            {
                firmId,
                userId,
                startDate,
                endDate
            }, cancellationToken: cancellationToken);
        }

        public Task<List<PurchaseInfoDto>> GetLastPurchaseInfoAsync(int firmId, int userId, List<PurchaseInfoRequestDto> purchaseInfoRequestDto)
        {
            return PostAsync<List<PurchaseInfoRequestDto>, List<PurchaseInfoDto>>($"/Upd/GetLastPurchaseInfo?firmId={firmId}&userId={userId}", purchaseInfoRequestDto);
        }

        public Task<List<UpdDto>> GetByCriterionAsync(int firmId, int userId, UpdRequestDto request)
        {
            return PostAsync<UpdRequestDto, List<UpdDto>>($"/Upd/GetByCriterion?firmId={firmId}&userId={userId}", request);
        }

        public Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || baseIds.Count == 0)
            {
                return Task.FromResult(new List<PaidSumDto>());
            }
            return PostAsync<IReadOnlyCollection<long>, List<PaidSumDto>>($"/Upd/GetPaidSumByBaseIds?firmId={firmId}&userId={userId}", baseIds);
        }
        
        public Task<string> GetNoTaxMessageAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<string>($"/Upd/GetNoTaxMessage?firmId={firmId}&userId={userId}&baseId={baseId}");
        }
    }
}