using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Requisites.ApiClient.Abstractions.Clients;

namespace Moedelo.Requisites.ApiClient.Clients
{
    [InjectAsSingleton(typeof(IStockVisibilityApiClient))]
    public class StockVisibilityApiClient: BaseApiClient, IStockVisibilityApiClient
    {
        private const string StockVisibilityController = "/api/v1/StockVisibility";

        public StockVisibilityApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<StockVisibilityApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("RequisitesApiEndpoint"),
                logger)
        {
        }
        
        /// <summary>
        ///  Возвращает состояние складского учета
        /// </summary>
        /// <param name="year">Возвращает состояние склада на указанный год</param>
        /// <returns>Скрыт или нет</returns>
        public async Task<bool> IsVisibleAsync(int? year = null)
        {
            var uri = $"{StockVisibilityController}";

            var result = await GetAsync<DataResponseWrapper<bool>>(uri, year);

            return result.Data;
        }
    }
}