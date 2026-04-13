using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Stock.ApiClient.Abstractions.legacy;
using Moedelo.Stock.ApiClient.legacy.models;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IStockSettingsApiCient))]
    public class StockSettingsApiClient : BaseLegacyApiClient, IStockSettingsApiCient
    {
        public StockSettingsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<StockSettingsApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("StockApiEndpoint"),
                logger)
        {
        }
        
        public async Task<bool> IsStockEnabledAsync(int firmId, int userId)
        {
            var response = await GetAsync<DataResponse<bool>>("/StockSettings/GetStockState", new { firmId, userId }).ConfigureAwait(false);
            return response.Data;
        }
    }
}