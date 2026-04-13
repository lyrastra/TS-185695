using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Marketplaces;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Client.Products
{
    [InjectAsSingleton]
    public class ProductMarketplaceCodeApiClient : BaseApiClient, IProductMarketplaceCodeApiClient
    {
        private readonly ISettingRepository settingRepository;

        public ProductMarketplaceCodeApiClient(IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, 
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager, ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("StockApiEndpoint").Value;
        }

        public Task<ProductMarketplaceCodeDto[]> GetByProductIdAndMarketplaceTypeAsync(
            int firmId,
            int userId,
            IReadOnlyCollection<long> productIds,
            MarketplaceType marketplaceType,
            bool onlyPrimaryCodeType = false)
        {
            return PostAsync<IReadOnlyCollection<long>, ProductMarketplaceCodeDto[]>(
                $"/ProductMarketplace/GetByProductIdsAndMarketplaceTypeAsync?firmId={firmId}&userId={userId}&type={(int)marketplaceType}&onlyPrimaryCodeType={onlyPrimaryCodeType}",
                productIds);
        }
    }
}
