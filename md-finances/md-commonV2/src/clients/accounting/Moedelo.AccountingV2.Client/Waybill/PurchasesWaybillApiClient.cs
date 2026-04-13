using Moedelo.AccountingV2.Dto.Waybills.Purchases;
using Moedelo.CommonV2.Extensions.System;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.Waybill
{
    [InjectAsSingleton]
    public class PurchasesWaybillApiClient : BaseApiClient, IPurchasesWaybillApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PurchasesWaybillApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        /// <inheritdoc />
        public Task<PurchasesWaybillCollectionDto> GetAsync(
            int firmId,
            int userId,
            uint pageNo = 1,
            uint pageSize = 50,
            string number = null,
            DateTime? docAfterDate = null,
            DateTime? docBeforeDate = null,
            DateTime? afterDate = null,
            DateTime? beforeDate = null,
            int? kontragentId = null,
            long? stockId = null, CancellationToken cancellationToken = default)
        {
            var url = new StringBuilder($"/api/v1/purchases/waybill?firmId={firmId}&userId={userId}");
            url.Append($"&pageNo={pageNo}");
            url.Append($"&pageSize={pageSize}");
            url.Append($"&number={number}");
            url.Append($"&docAfterDate={docAfterDate?.ToString("yyyy-MM-dd")}");
            url.Append($"&docBeforeDate={docBeforeDate?.ToString("yyyy-MM-dd")}");
            url.Append($"&afterDate={afterDate?.ToStrictString()}");
            url.Append($"&beforeDate={beforeDate?.ToStrictString()}");
            url.Append($"&kontragentId={kontragentId}");
            url.Append($"&stockId={stockId}");

            return GetAsync<PurchasesWaybillCollectionDto>(url.ToString(), cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public Task<PurchasesWaybillDto> GetByBaseIdAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<PurchasesWaybillDto>($"/api/v1/purchases/waybill/{baseId}?firmId={firmId}&userId={userId}");
        }

        /// <inheritdoc />
        public Task<PurchasesWaybillDto> SaveAsync(int firmId, int userId, PurchasesWaybillSaveRequestDto dto, HttpQuerySetting setting = null)
        {
            if (dto.Id != 0)
            {
                throw new NotImplementedException("Saving of existent waybill is not implemented. Waiting for PutAsync");
            }

            return PostAsync<PurchasesWaybillSaveRequestDto, PurchasesWaybillDto>(
                $"/api/v1/purchases/waybill?firmId={firmId}&userId={userId}", 
                dto, 
                setting: setting);
        }
        
        public Task<List<PurchasesWaybillItemsDto>> GetItemsByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || baseIds.Count == 0)
            {
                return Task.FromResult(new List<PurchasesWaybillItemsDto>());
            }
            return PostAsync<IReadOnlyCollection<long>, List<PurchasesWaybillItemsDto>>($"/api/v1/purchases/waybill/byIds/items?firmId={firmId}&userId={userId}", baseIds);
        }
    }
}