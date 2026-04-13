using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PurchaseInfo;
using Moedelo.AccountingV2.Dto.Waybills;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Waybill
{
    [InjectAsSingleton]
    public class WaybillApiClient : BaseApiClient, IWaybillApiClient
    {
        private readonly SettingValue apiEndPoint;

        public WaybillApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        public Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/WaybillClient/Provide?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task ProvidePostingsOnlyAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/WaybillClient/ProvidePostingsOnly?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task DeleteByIdAsync(int firmId, int userId, int id)
        {
            return DeleteAsync("/WaybillClient/DeleteById", new {firmId, userId,id});
        }

        public Task DeleteByBaseIdAsync(int firmId, int userId, long baseId)
        {
            return DeleteAsync("/WaybillClient/DeleteByBaseId", new { firmId, userId, baseId });
        }

        public Task<List<WaybillDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds,
            HttpQuerySetting querySettings,
            CancellationToken cancellationToken)
        {
            var uri = $"/WaybillApiV2/GetByBaseIds?firmId={firmId}&userId={userId}";

            return PostAsync<IEnumerable<long>, List<WaybillDto>>(uri, baseIds,
                setting: querySettings,
                cancellationToken: cancellationToken);
        }

        public Task SetStockAsync(int firmId, int userId, long documentBaseId, long stockId)
        {
            var requestDto = new WaybillStockDto {DocumentBaseId = documentBaseId, StockId = stockId};

            return PostAsync($"/WaybillClient/SetStock?firmId={firmId}&userId={userId}", requestDto);
        }

        public Task<List<PurchaseInfoResponseDto>> GetLastPurchaseInfoAsync(int firmId, int userId, List<PurchaseInfoRequestDto> purchaseInfoRequests)
        {
            if (purchaseInfoRequests?.Any() != true)
            {
                return Task.FromResult(new List<PurchaseInfoResponseDto>());
            }

            return PostAsync<List<PurchaseInfoRequestDto>, List<PurchaseInfoResponseDto>>($"/WaybillClient/GetLastPurchaseInfo?firmId={firmId}&userId={userId}", purchaseInfoRequests);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}