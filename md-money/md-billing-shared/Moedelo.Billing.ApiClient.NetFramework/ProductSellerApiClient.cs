using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Legacy.Dto.ProductSeller;
using Moedelo.Billing.Abstractions.Legacy.Interfaces.ProductSeller;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework
{
    [InjectAsSingleton(typeof(IProductSellerApiClient))]
    public class ProductSellerApiClient : BaseApiClient, IProductSellerApiClient
    {
        private readonly SettingValue apiEndPoint;

        public ProductSellerApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<IReadOnlyCollection<ProductSellerDto>> GetSellersAsync(IReadOnlyCollection<string> productCodes)
        {
            return PostAsync<IReadOnlyCollection<string>, IReadOnlyCollection<ProductSellerDto>>(
                "/ProductSeller/GetSellers", productCodes);
        }

        public Task<IReadOnlyCollection<ProductSellerDto>> GetProductsAsync(string sellerCode)
        {
            return GetAsync<IReadOnlyCollection<ProductSellerDto>>(
                "/ProductSeller/GetProducts", new { sellerCode });
        }
    }
}