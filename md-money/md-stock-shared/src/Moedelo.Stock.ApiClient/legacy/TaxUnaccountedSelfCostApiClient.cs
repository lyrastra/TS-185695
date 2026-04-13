using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Stock.ApiClient.Abstractions.legacy;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.SelfCost;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(ITaxUnaccountedSelfCostApiClient))]
    public class TaxUnaccountedSelfCostApiClient: BaseLegacyApiClient, ITaxUnaccountedSelfCostApiClient
    {
        public TaxUnaccountedSelfCostApiClient(
            IHttpRequestExecuter httpRequestExecutor, 
            IUriCreator uriCreator, 
            IAuditTracer auditTracer, 
            IAuditHeadersGetter auditHeadersGetter, 
            ISettingRepository settingRepository, 
            ILogger<TaxUnaccountedSelfCostApiClient> logger) 
            : base(
                httpRequestExecutor, 
                uriCreator, 
                auditTracer, 
                auditHeadersGetter, 
                settingRepository.Get("StockApiEndpoint"),
                logger)
        {
        }

        public Task SaveAsync(FirmId firmId, UserId userId, IReadOnlyCollection<SelfCostTaxUnaccountedSaveRequestDto> requests)
        {
            // пустой список считается корректным допустимым значением
            if (requests == null)
            {
                return Task.CompletedTask;
            }

            return PostAsync(
                $"/SelfCost/TaxUnaccounted/Save?firmId={firmId}&userId={userId}",
                requests);
        }

        public Task<List<SelfCostTaxUnaccountedDto>> GetAsync(FirmId firmId, UserId userId)
        {
            return GetAsync<List<SelfCostTaxUnaccountedDto>>($"/SelfCost/TaxUnaccounted/Get?firmId={firmId}&userId={userId}");
        }
    }
}