using Moedelo.AccountingV2.Dto.Waybills.Sales;
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
    public class SalesWaybillApiClient : BaseApiClient, ISalesWaybillApiClient
    {
        private readonly SettingValue apiEndPoint;

        public SalesWaybillApiClient(
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

        public Task<SalesWaybillCollectionDto> GetAsync(int firmId,
            int userId,
            uint pageNo = 1U,
            uint pageSize = 50U,
            string number = null,
            DateTime? docAfterDate = null,
            DateTime? docBeforeDate = null,
            DateTime? afterDate = null,
            DateTime? beforeDate = null,
            int? kontragentId = null, CancellationToken cancellationToken = default)
        {
            var url = new StringBuilder($"/api/v1/sales/waybill?firmId={firmId}&userId={userId}");
            url.Append($"&pageNo={pageNo}");
            url.Append($"&pageSize={pageSize}");
            url.Append($"&number={number}");
            url.Append($"&docAfterDate={docAfterDate?.ToString("yyyy-MM-dd")}");
            url.Append($"&docBeforeDate={docBeforeDate?.ToString("yyyy-MM-dd")}");
            url.Append($"&afterDate={afterDate?.ToStrictString()}");
            url.Append($"&beforeDate={beforeDate?.ToStrictString()}");
            url.Append($"&kontragentId={kontragentId}");

            return GetAsync<SalesWaybillCollectionDto>(url.ToString(), cancellationToken: cancellationToken);
        }

        public Task<SalesWaybillDto> GetByBaseIdAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<SalesWaybillDto>($"/api/v1/sales/waybill/{baseId}?firmId={firmId}&userId={userId}");
        }

        public Task<SalesWaybillDto> SaveAsync(int firmId, int userId, SalesWaybillSaveRequestDto dto, HttpQuerySetting setting = null)
        {
            if (dto.Id != 0)
            {
                throw new NotImplementedException("Saving of existent waybill is not implemented. Waiting for PutAsync");
            }

            return PostAsync<SalesWaybillSaveRequestDto, SalesWaybillDto>(
                $"/api/v1/sales/waybill?firmId={firmId}&userId={userId}", 
                dto, 
                setting: setting);
        }

        public Task<List<SalesWaybillItemsDto>> GetItemsByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || baseIds.Count == 0)
            {
                return Task.FromResult(new List<SalesWaybillItemsDto>());
            }
            return PostAsync<IReadOnlyCollection<long>, List<SalesWaybillItemsDto>>($"/api/v1/sales/waybill/byIds/items?firmId={firmId}&userId={userId}", baseIds);
        }
    }
}