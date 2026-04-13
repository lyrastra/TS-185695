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
    public class SupplierOrdersClient : BaseCoreApiClient, ISupplierOrdersClient
    {
        private const string prefix = "/private/api/v1/SupplierOrders";

        private readonly ISettingRepository settingRepository;

        public SupplierOrdersClient(IHttpRequestExecutor httpRequestExecutor,
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

        public async Task<List<SupplierOrderDto>> GetListAsync(int firmId, int userId)
        {
            var uri = $"{prefix}/GetList";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            return await GetAsync<List<SupplierOrderDto>>(uri, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task MergeItemsAsync(int firmId, int userId, ProductMergeRequestDto mergeRequest)
        {
            var uri = $"{prefix}/MergeProducts";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await PostAsync(uri, mergeRequest, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task<List<OrderToProductReferenceResponse>> GetSupplierOrdersToProductsReferencesAsync(int firmId, int userId, OrdersToProductsReferencesRequest request)
        {
            var uri = $"{prefix}/GetSupplierOrdersToProductsReferences";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            return await PostAsync<OrdersToProductsReferencesRequest, List<OrderToProductReferenceResponse>>(uri, request, queryHeaders: tokenHeaders)
                .ConfigureAwait(false);
        }
    }
}