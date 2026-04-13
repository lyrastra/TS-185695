using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Bills.Simple.SalesInvoice;
using Moedelo.AccountingV2.Dto.Invoices.Sales;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Invoice
{
    [InjectAsSingleton]
    public class SalesInvoiceApiClient : BaseApiClient, ISalesInvoiceApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public SalesInvoiceApiClient(
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

        public Task<SalesInvoiceCollectionDto> GetAsync(
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
            var url = new StringBuilder($"/api/v1/sales/invoice/common?firmId={firmId}&userId={userId}");
            url.Append($"&pageNo={pageNo}");
            url.Append($"&pageSize={pageSize}");
            url.Append($"&number={number}");
            url.Append($"&docAfterDate={docAfterDate}");
            url.Append($"&docBeforeDate={docBeforeDate}");
            url.Append($"&afterDate={afterDate}");
            url.Append($"&beforeDate={beforeDate}");
            url.Append($"&kontragentId={kontragentId}");

            return GetAsync<SalesInvoiceCollectionDto>(url.ToString());
        }

        /// <summary>
        /// Возвращает список облегченных моделей счетов с позициями.
        /// Метод используется в md-stock для журнала документов на странице Товароучёт-Движения.
        /// </summary>
        public Task<List<SalesInvoiceSimpleDto>> GetWithItemsAsync(int firmId, int userId, uint pageNo = 1, uint pageSize = 50, string number = null,
            DateTime? docAfterDate = null, DateTime? docBeforeDate = null, DateTime? afterDate = null,
            DateTime? beforeDate = null, int? kontragentId = null)
        {
            var url = new StringBuilder($"/api/v1/sales/invoice/common/GetWithItems?firmId={firmId}&userId={userId}");
            url.Append($"&pageNo={pageNo}");
            url.Append($"&pageSize={pageSize}");
            url.Append($"&number={number}");
            url.Append($"&docAfterDate={docAfterDate}");
            url.Append($"&docBeforeDate={docBeforeDate}");
            url.Append($"&afterDate={afterDate}");
            url.Append($"&beforeDate={beforeDate}");
            url.Append($"&kontragentId={kontragentId}");

            return GetAsync<List<SalesInvoiceSimpleDto>>(url.ToString());
        }

        public Task<SalesInvoiceDto> GetByBaseIdAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<SalesInvoiceDto>($"/api/v1/sales/invoice/common/{baseId}?firmId={firmId}&userId={userId}");
        }

        public Task<List<SalesInvoiceDto>> GetByBaseIdsWithItemsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || baseIds.Any() == false)
            {
                return Task.FromResult(new List<SalesInvoiceDto>());
            }

            var uri = $"/SalesInvoice/Common/GetByBaseIdsWithItems?firmId={firmId}&userId={userId}";

            return PostAsync<IReadOnlyCollection<long>, List<SalesInvoiceDto>>(uri, baseIds);
        }

        public Task<SalesInvoiceDto> SaveAsync(int firmId, int userId, SalesInvoiceSaveRequestDto dto)
        {
            if (dto.Id != 0)
            {
                throw new NotImplementedException("Saving of existent invoice is not implemented. Waiting for PutAsync");
            }

            return PostAsync<SalesInvoiceSaveRequestDto, SalesInvoiceDto>($"/api/v1/sales/invoice/common?firmId={firmId}&userId={userId}", dto);
        }
    }
}
