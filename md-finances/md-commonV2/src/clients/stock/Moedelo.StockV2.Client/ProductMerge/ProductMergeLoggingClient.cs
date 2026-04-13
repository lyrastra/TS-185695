using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Client.ProductMerge;
using Moedelo.StockV2.Dto.ProductMerge;
using System.Threading.Tasks;

namespace Moedelo.StockV2.Client.Operations
{
    [InjectAsSingleton]
    public class ProductMergeLoggingClient : BaseApiClient, IProductMergeLoggingClient
    {
        private readonly SettingValue apiEndPoint;

        public ProductMergeLoggingClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) :
            base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("StockServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task LogAsync(int firmId, int userId, int mergeId, LogsDto logs)
        {
            return PostAsync($"/ProductMerge/Log?firmId={firmId}&userId={userId}&mergeId={mergeId}", logs);
        }
    }
}