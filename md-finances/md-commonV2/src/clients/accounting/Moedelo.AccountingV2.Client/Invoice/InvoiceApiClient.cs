using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.AccountingV2.Dto.Invoice;
using Moedelo.Common.Enums.Enums;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Invoice
{
    [InjectAsSingleton]
    public class InvoiceApiClient : BaseApiClient, IInvoiceApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public InvoiceApiClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task ProvideAsync(int firmId, int userId, long invoiceBaseId)
        {
            return ProvideAsync(firmId, userId, new List<long> { invoiceBaseId });
        }

        public Task DeleteByBaseId(int firmId, int userId, List<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/InvoiceApi/DeleteByBaseId?firmId={firmId}&userId={userId}", documentBaseIds);
        }

        public Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> invoiceBaseIds)
        {
            if (invoiceBaseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/InvoiceApi/Provide?firmId={firmId}&userId={userId}", invoiceBaseIds);
        }

        public Task<List<QuarterDto>> GetQuartersWithInvoices(int firmId, int userId)
        {
            return GetAsync<List<QuarterDto>>($"/InvoiceApi/GetQuartersWithInvoices", new { firmId, userId });
        }

        public async Task<byte[]> DownloadFileAsync(int firmId, int userId, long documentBaseId, bool useStampAndSign, DocumentFormat format)
        {
            var response = await GetAsync<DocFileInfoDto>("/InvoiceApi/Download", new
            {
                firmId,
                userId,
                documentBaseId,
                useStampAndSign,
                format
            }).ConfigureAwait(false);

            return response?.File;
        }

        public Task MergeProductsAsync(int firmId, int userId, ProductMergeRequestDto data)
        {
            return PostAsync<ProductMergeRequestDto>($"/InvoiceApi/MergeProducts?firmId={firmId}&userId={userId}", data);
        }
    }
}