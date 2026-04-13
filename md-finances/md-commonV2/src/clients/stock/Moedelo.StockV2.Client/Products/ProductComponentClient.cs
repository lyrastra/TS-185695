using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Client.ResponseWrappers;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Client.Products
{
    [InjectAsSingleton]
    public class ProductComponentClient : BaseApiClient, IProductComponentClient
    {
        private readonly SettingValue apiEndPoint;

        public ProductComponentClient(
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

        public async Task<List<ProductComponentDto>> GetAsync(int firmId, int userId, List<long> productIds)
        {
            if (productIds == null || !productIds.Any())
            {
                return new List<ProductComponentDto>();
            }

            var response = await PostAsync<IEnumerable<long>, List<ProductComponentDto>>(
                $"/ProductComponent/ByProductIds?firmId={firmId}&userId={userId}",
                productIds).ConfigureAwait(false);

            return response;
        }
    }
}