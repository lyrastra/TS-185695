using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Legacy.Dto.ProductSeller;
using Moedelo.Billing.Abstractions.Legacy.Interfaces.ProductSeller;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.Legacy;

[InjectAsSingleton(typeof(IProductSellerApiClient))]
internal class ProductSellerApiClient : BaseLegacyApiClient, IProductSellerApiClient
{
    public ProductSellerApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<ProductSellerApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            auditHeadersGetter,
            settingRepository.Get("InternalBillingApiEndpoint"),
            logger)
    {
    }

    public Task<IReadOnlyCollection<ProductSellerDto>> GetSellersAsync(IReadOnlyCollection<string> productCodes)
    {
        return PostAsync<IReadOnlyCollection<string>, IReadOnlyCollection<ProductSellerDto>>(
            "/ProductSeller/GetSellers",
            productCodes);
    }

    public Task<IReadOnlyCollection<ProductSellerDto>> GetProductsAsync(string sellerCode)
    {
        return GetAsync<IReadOnlyCollection<ProductSellerDto>>(
            "/ProductSeller/GetProducts",
            new { sellerCode });
    }
}