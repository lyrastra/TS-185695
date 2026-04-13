using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Invoices;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Invoices.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.legacy.Purchases.Invoices
{
    [InjectAsSingleton(typeof(IPurchasesInvoiceApiClient))]
    public class PurchasesInvoiceApiClient : BaseLegacyApiClient, IPurchasesInvoiceApiClient
    {
        public PurchasesInvoiceApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PurchasesInvoiceApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<PurchasesInvoiceResponseDto> SaveAsync(FirmId firmId, UserId userId, PurchasesInvoiceSaveRequestDto dto)
        {
            var uri = $"/api/v1/purchases/invoice/common?firmId={firmId}&userId={userId}";
            return PostAsync<PurchasesInvoiceSaveRequestDto, PurchasesInvoiceResponseDto>(uri, dto);
        }

        public Task<List<PurchasesInvoiceResponseDto>> GetByBaseIdsWithItemsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            var uri = $"/PurchasesInvoice/Common/GetByBaseIdsWithItems?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, List<PurchasesInvoiceResponseDto>>(uri, baseIds);
        }
    }
}