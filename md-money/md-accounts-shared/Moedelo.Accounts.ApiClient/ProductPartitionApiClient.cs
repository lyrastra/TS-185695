using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Accounts.ApiClient.Enums;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounts.Clients
{
    [InjectAsSingleton(typeof(IProductPartitionApiClient))]
    internal sealed class ProductPartitionApiClient : BaseLegacyApiClient, IProductPartitionApiClient
    {
        public ProductPartitionApiClient(
                IHttpRequestExecuter httpRequestExecuter,
                IUriCreator uriCreator,
                IAuditTracer auditTracer,
                IAuditHeadersGetter auditHeadersGetter,
                ISettingRepository settingRepository,
                ILogger<ProductPartitionApiClient> logger
            )
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountPrivateApiEndpoint"),
                logger)
        {
        }

        /// <summary> 
        /// Возвращает product partition пользователя
        /// </summary>
        public Task<WLProductPartition> GetAsync(int userId, int firmId = 0)
        {
            return GetAsync<WLProductPartition>("/Rest/ProductPartition/Get", new { userId, firmId });
        }
    }
}