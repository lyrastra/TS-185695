using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Invoice;
using Moedelo.Docs.ApiClient.legacy.Sales.Invoices;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Docs.ApiClient.legacy.Invoice
{
    [InjectAsSingleton(typeof(IInvoiceApiClient))]
    public class InvoiceApiClient : BaseApiClient, IInvoiceApiClient
    {   
        public InvoiceApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalesInvoiceApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }
        
        public Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> invoiceBaseIds)
        {
            if (invoiceBaseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/InvoiceApi/Provide?firmId={firmId}&userId={userId}", invoiceBaseIds, setting: new HttpQuerySetting());
        }
    }
}