using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Rosstat;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.Rosstat
{
    [InjectAsSingleton]
    public class RosstatApiClient : BaseApiClient, IRosstatApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public RosstatApiClient(
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
            return apiEndPoint.Value + "/Rosstat/V2";
        }

        public Task<RosstatDepartmentDto> GetDepartmentByOktmoAsync(string oktmo)
        {
            return GetAsync<RosstatDepartmentDto>("/GetDepartmentByOktmo", new {oktmo});
        }

        public Task<List<RosstatDepartmentDto>> GetDepartmentListByRegionCodeAsync(string regionCode)
        {
            return GetAsync<List<RosstatDepartmentDto>>("/GetDepartmentListByRegionCode", new {regionCode});
        }
    }
}