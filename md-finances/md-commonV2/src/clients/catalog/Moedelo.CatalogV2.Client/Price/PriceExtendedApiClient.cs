using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Price;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.Price
{
    [InjectAsSingleton]
    public class PriceExtendedApiClient : BaseApiClient, IPriceExtendedApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public PriceExtendedApiClient(
             IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                 )
        {
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/PriceExtended/V2";
        }

        public Task<PriceExtendedDto> GetByPriceIdAsync(int priceId)
        {
            return GetAsync<PriceExtendedDto>("/GetByPriceId", new {priceId});
        }

        public Task<List<PriceExtendedDto>> GetListByCriterionAsync(GetPriceExtendedCriterionDto criterionDto)
        {
            return PostAsync<GetPriceExtendedCriterionDto, List<PriceExtendedDto>>("/GetListByCriterion", criterionDto);
        }
    }
}