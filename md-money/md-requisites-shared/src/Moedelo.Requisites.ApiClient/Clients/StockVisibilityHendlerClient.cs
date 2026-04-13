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
    [InjectAsSingleton(typeof(IStockVisibilityHendlerClient))]
    public class StockVisibilityHendlerClient: BaseApiClient, IStockVisibilityHendlerClient
    {
        private const string PrivateStockVisibilityController = "/private/api/v1/StockVisibility";
       
        public StockVisibilityHendlerClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<StockVisibilityHendlerClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("RequisitesHandlerEndpoint"),
                logger)
        {
        }

        /// <summary>
        ///  Возвращает список FirmId фирм с выключенным складом
        /// </summary>
        public async Task<int[]> GetAllFirmsWithInvisibleStockAsync()
        {
            var uri = $"{PrivateStockVisibilityController}";
            var result = await GetAsync<DataResponseWrapper<int[]>>($"{uri}/AllFirms");
            return result.Data;
        }
    }
}