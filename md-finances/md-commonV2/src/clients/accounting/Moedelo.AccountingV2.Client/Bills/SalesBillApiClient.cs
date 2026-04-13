using Moedelo.AccountingV2.Dto.Bills;
using Moedelo.AccountingV2.Dto.Bills.Simple.SalesBill;
using Moedelo.CommonV2.Extensions.System;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.Bills
{
    [InjectAsSingleton]
    public class SalesBillApiClient : BaseApiClient, ISalesBillApiClient
    {
        private readonly SettingValue apiEndPoint;

        public SalesBillApiClient(
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

        public Task<SalesBillCollectionDto> GetAsync(int firmId, int userId, SalesBillPaginationRequestDto criteria)
        {
            var url = new StringBuilder($"/api/v1/sales/bill?firmId={firmId}&userId={userId}");
            url.Append($"&pageNo={criteria.PageNo}");
            url.Append($"&pageSize={criteria.PageSize}");
            url.Append($"&AfterDate={criteria.AfterDate?.ToStrictString()}");
            url.Append($"&BeforeDate={criteria.BeforeDate?.ToStrictString()}");
            url.Append($"&docAfterDate={criteria.DocAfterDate?.ToString("yyyy-MM-dd")}");
            url.Append($"&docBeforeDate={criteria.DocBeforeDate?.ToString("yyyy-MM-dd")}");
            url.Append($"&Number={criteria.Number}");
            url.Append($"&KontragentId={criteria.KontragentId}");

            return GetAsync<SalesBillCollectionDto>(url.ToString());
        }
        
        /// <inheritdoc />
        public Task<SalesBillFullCollectionDto> GetForInternalAsync(int firmId, int userId, SalesBillPaginationRequestDto criteria)	
        {
            var url = new StringBuilder($"/api/v1/sales/bill/GetForInternal?firmId={firmId}&userId={userId}");
            url.Append($"&pageNo={criteria.PageNo}");
            url.Append($"&pageSize={criteria.PageSize}");
            url.Append($"&AfterDate={criteria.AfterDate?.ToStrictString()}");
            url.Append($"&BeforeDate={criteria.BeforeDate?.ToStrictString()}");
            url.Append($"&docAfterDate={criteria.DocAfterDate?.ToString("yyyy-MM-dd")}");
            url.Append($"&docBeforeDate={criteria.DocBeforeDate?.ToString("yyyy-MM-dd")}");
            url.Append($"&Number={criteria.Number}");
            url.Append($"&KontragentId={criteria.KontragentId}");

            return GetAsync<SalesBillFullCollectionDto>(url.ToString());
        }

        /// <inheritdoc />
        public Task<List<SalesBillSimpleDto>> GetWithItemsAsync(int firmId, int userId,
            SalesBillPaginationRequestDto criteria, CancellationToken cancellationToken)
        {
            var url = new StringBuilder($"/api/v1/sales/bill/GetWithItems?firmId={firmId}&userId={userId}");
            url.Append($"&pageNo={criteria.PageNo}");
            url.Append($"&pageSize={criteria.PageSize}");
            url.Append($"&AfterDate={criteria.AfterDate?.ToStrictString()}");
            url.Append($"&BeforeDate={criteria.BeforeDate?.ToStrictString()}");
            url.Append($"&docAfterDate={criteria.DocAfterDate?.ToString("yyyy-MM-dd")}");
            url.Append($"&docBeforeDate={criteria.DocBeforeDate?.ToString("yyyy-MM-dd")}");
            url.Append($"&Number={criteria.Number}");
            url.Append($"&KontragentId={criteria.KontragentId}");

            return GetAsync<List<SalesBillSimpleDto>>(url.ToString(), cancellationToken: cancellationToken);
        }

        public Task<List<SalesBillItemsDto>> GetItemsByBillBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<SalesBillItemsDto>());
            }
            
            return PostAsync<IReadOnlyCollection<long>, List<SalesBillItemsDto>>(
                $"/api/v1/sales/bill/byIds/items?firmId={firmId}&userId={userId}",
                baseIds);
        }

        public Task<SalesBillDto> SaveAsync(int firmId, int userId, SalesBillSaveRequestDto dto)
        {
            if (dto.Id != 0)
            {
                throw new NotImplementedException("Saving of existent bill is not implemented. Waiting for PutAsync");
            }

            return PostAsync<SalesBillSaveRequestDto, SalesBillDto>($"/api/v1/sales/bill?firmId={firmId}&userId={userId}", dto);
        }

        public Task<SalesBillDto> GetByBaseIdAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<SalesBillDto>($"/api/v1/sales/bill/{baseId}?firmId={firmId}&userId={userId}");
        }

        public Task<List<SalesBillDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<SalesBillDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<SalesBillDto>>(
                $"/api/v1/sales/bill/byIds?firmId={firmId}&userId={userId}",
                baseIds);
        }

        public Task UpdateStatusAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/Bill/UpdateCoverageStatus?firmId={firmId}&userId={userId}", baseIds);
        }
        
        public Task UpdatePaymentStatusAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/Bill/UpdatePaymentStatus?firmId={firmId}&userId={userId}", baseIds);
        }
    }
}