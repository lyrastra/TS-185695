using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Region;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.Region
{
    [InjectAsSingleton]
    public class RegionApiClient : BaseApiClient, IRegionApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public RegionApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        public Task<List<RegionDto>> GetAllAsync()
        {
            return GetAsync<List<RegionDto>>("/Get");
        }

        public Task<RegionDto> GetByIdAsync(int id)
        {
            return GetAsync<RegionDto>("/GetById", new {id});
        }

        public Task<List<RegionDto>> GetByIdsAsync(IReadOnlyCollection<int> regionIds)
        {
            if (!regionIds.Any())
            {
                return Task.FromResult(new List<RegionDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<RegionDto>>("/GetByIds", regionIds);
        }

        public Task<RegionDto> GetByCodeAsync(string code)
        {
            return GetAsync<RegionDto>("/GetByCode", new {code});
        }

        public Task<RegionDto> GetByPhoneAsync(string phone)
        {
            return GetAsync<RegionDto>("/GetByPhone", new {phone});
        }

        public Task<int?> GetRegionIdByIPAddressAsync(string ipAddress)
        {
            return GetAsync<int?>("/GetRegionIdByIPAddress", new { ipAddress });
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Region/V2";
        }
    }
}