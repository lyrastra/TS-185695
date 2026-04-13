using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Bills.Simple.PurchasesInvoice;
using Moedelo.AccountingV2.Dto.Invoices.Purchases;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Invoice
{
    [InjectAsSingleton]
    public class PurchasesInvoiceApiClient : BaseApiClient, IPurchasesInvoiceApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public PurchasesInvoiceApiClient(
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

        public Task<PurchasesCommonInvoiceCollectionDto> GetAsync(
            int firmId,
            int userId,
            uint pageNo = 1,
            uint pageSize = 50,
            string number = null,
            DateTime? docAfterDate = null,
            DateTime? docBeforeDate = null,
            DateTime? afterDate = null,
            DateTime? beforeDate = null,
            int? kontragentId = null)
        {
            var url = new StringBuilder($"/api/v1/purchases/invoice/common?firmId={firmId}&userId={userId}");
            url.Append($"&pageNo={pageNo}");
            url.Append($"&pageSize={pageSize}");
            url.Append($"&number={number}");
            url.Append($"&docAfterDate={docAfterDate}");
            url.Append($"&docBeforeDate={docBeforeDate}");
            url.Append($"&afterDate={afterDate}");
            url.Append($"&beforeDate={beforeDate}");
            url.Append($"&kontragentId={kontragentId}");

            return GetAsync<PurchasesCommonInvoiceCollectionDto>(url.ToString());
        }

        public Task<PurchasesCommonInvoiceDto> GetByBaseIdAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<PurchasesCommonInvoiceDto>($"/api/v1/purchases/invoice/common/{baseId}?firmId={firmId}&userId={userId}");
        }

        public Task<List<PurchasesCommonInvoiceDto>> GetByBaseIdsWithItemsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || baseIds.Any() == false)
            {
                return Task.FromResult(new List<PurchasesCommonInvoiceDto>());
            }

            var uri = $"/PurchasesInvoice/Common/GetByBaseIdsWithItems?firmId={firmId}&userId={userId}";

            return PostAsync<IReadOnlyCollection<long>, List<PurchasesCommonInvoiceDto>>(uri, baseIds);
        }

        public Task<List<PurchasesInvoiceSimpleDto>> GetWithItemsAsync(int firmId, int userId, uint pageNo = 1U,
            uint pageSize = 50U, string number = null,
            DateTime? docAfterDate = null, DateTime? docBeforeDate = null, DateTime? afterDate = null,
            DateTime? beforeDate = null, int? kontragentId = null, CancellationToken cancellationToken = default)
        {
            var url = new StringBuilder($"/api/v1/purchases/invoice/common/GetWithItems?firmId={firmId}&userId={userId}");
            url.Append($"&pageNo={pageNo}");
            url.Append($"&pageSize={pageSize}");
            url.Append($"&number={number}");
            url.Append($"&docAfterDate={docAfterDate}");
            url.Append($"&docBeforeDate={docBeforeDate}");
            url.Append($"&afterDate={afterDate}");
            url.Append($"&beforeDate={beforeDate}");
            url.Append($"&kontragentId={kontragentId}");

            return GetAsync<List<PurchasesInvoiceSimpleDto>>(url.ToString(), cancellationToken: cancellationToken);
        }

        public Task<PurchasesCommonInvoiceDto> SaveAsync(int firmId, int userId, PurchasesCommonInvoiceSaveRequestDto dto)
        {
            if (dto.Id != 0)
            {
                throw new NotImplementedException("Saving of existent invoice is not implemented. Waiting for PutAsync");
            }

            return PostAsync<PurchasesCommonInvoiceSaveRequestDto, PurchasesCommonInvoiceDto>($"/api/v1/purchases/invoice/common?firmId={firmId}&userId={userId}", dto);
        }
    }
}
