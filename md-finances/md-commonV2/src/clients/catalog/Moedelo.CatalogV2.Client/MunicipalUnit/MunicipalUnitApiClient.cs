using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.MunicipalUnit;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.MunicipalUnit
{
    /// <summary>
    /// Работа с муниципальными районами: муниципальный округ Беговой, Белинский муниципальный район ...
    /// </summary>
    [InjectAsSingleton]
    public class MunicipalUnitApiClient : BaseApiClient, IMunicipalUnitApiClient
    {
        private readonly SettingValue apiEndPoint;

        public MunicipalUnitApiClient(
            ISettingRepository settingRepository,
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, 
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/MunicipalUnit/V2";
        }

        public Task<MunicipalUnitDto> GetByOktmoAsync(string oktmo)
        {
            return GetAsync<MunicipalUnitDto>("/GetByOktmo", new { oktmo });
        }
    }
}