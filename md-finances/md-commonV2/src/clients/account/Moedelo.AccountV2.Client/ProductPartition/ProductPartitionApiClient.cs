using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Products;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.ProductPartition
{
    [InjectAsSingleton]
    public class ProductPartitionApiClient : BaseApiClient, IProductPartitionApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public ProductPartitionApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/ProductPartition";
        }

        public Task<WLProductPartition> GetAsync(int userId, int firmId = 0)
        {
            return GetAsync<WLProductPartition>("/Get", new { userId, firmId });
        }
    }
}