using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.StockV2.Dto.Operations.ProductMerge;
using Moedelo.StockV2.Dto.StockOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.StockV2.Client.Orders
{
    [InjectAsSingleton]
    public class CustomerOrdersPacksClient : BaseCoreApiClient, ICustomerOrdersPacksClient
    {
        private const string prefix = "/private/api/v1/CustomerOrdersPacks";

        private readonly ISettingRepository settingRepository;

        public CustomerOrdersPacksClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("StockOrdersApiEndpoint").Value;
        }

        public async Task MergeItemsAsync(int firmId, int userId, ProductMergeRequestDto mergeRequest)
        {
            var uri = $"{prefix}/MergeProducts";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await PostAsync(uri, mergeRequest, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task<List<OrderToProductReferenceResponse>> GetOrderPacksToProductsReferencesAsync(int firmId, int userId, OrdersToProductsReferencesRequest request)
        {
            var uri = $"{prefix}/GetOrderPacksToProductsReferences";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            return await PostAsync<OrdersToProductsReferencesRequest, List<OrderToProductReferenceResponse>>(uri, request, queryHeaders: tokenHeaders)
                .ConfigureAwait(false);
        }
    }
}
