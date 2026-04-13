
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading.Tasks;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.ProductIncome;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.ProductIncome.Models;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Types;

namespace Moedelo.Docs.ApiClient.legacy.Purchases.PurchasesProductIncome
{
    [InjectAsSingleton(typeof(IPurchasesProductIncomeApiClient))]
    public class PurchasesProductIncomeApiClient : BaseLegacyApiClient, IPurchasesProductIncomeApiClient
    {
        public PurchasesProductIncomeApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PurchasesProductIncomeApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("DocsApiEndpoint"),
                logger)
        {
        }

        public Task CreateAsync(FirmId firmId, UserId userId, ProductIncomeCreateDto dto)
        {
            if (dto == null)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/api/v1/Purchases/ProductIncome?firmId={firmId}&userId={userId}", dto);
        }

    }
}
