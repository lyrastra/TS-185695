using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Invoices;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Invoices.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.legacy.Sales.Invoices
{
    [InjectAsSingleton(typeof(ISalesInvoiceApiClient))]
    public class SalesInvoiceApiClient : BaseLegacyApiClient, ISalesInvoiceApiClient
    {
        public SalesInvoiceApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalesInvoiceApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public async Task<byte[]> DownloadFileAsync(
            FirmId firmId,
            UserId userId,
            long documentBaseId,
            bool useStampAndSign,
            SalesInvoiceFileFormat format)
        {
            var response = await GetAsync<DocFileInfoDto>("/InvoiceApi/Download", new
            {
                firmId,
                userId,
                documentBaseId,
                useStampAndSign,
                format
            });

            return response?.File;
        }

        public async Task<SalesInvoiceDto> GetByBaseIdAsync(FirmId firmId, UserId userId,long id)
        {
            var result =
                await GetAsync<SalesInvoiceDto>($"/api/v1/sales/invoice/common/{id}", new
                {
                    firmId,
                    userId
                });
            return result;
        }

        public Task<SalesInvoiceDto> SaveAsync(FirmId firmId, UserId userId, SalesInvoiceSaveRequestDto dto)
        {
            return PostAsync<SalesInvoiceSaveRequestDto, SalesInvoiceDto>(
                $"/api/v1/sales/invoice/common?firmId={firmId}&userId={userId}", 
                dto);
        }

        public Task<List<SalesInvoiceDto>> GetByBaseIdsWithItemsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            var uri = $"/SalesInvoice/Common/GetByBaseIdsWithItems?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, List<SalesInvoiceDto>>(uri, baseIds);
        }
    }
}