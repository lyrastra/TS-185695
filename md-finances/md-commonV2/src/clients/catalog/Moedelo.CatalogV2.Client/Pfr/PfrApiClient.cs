using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Pfr;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.Pfr
{
    [InjectAsSingleton]
    public class PfrApiClient : BaseApiClient, IPfrApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public PfrApiClient(
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
            return apiEndPoint.Value+ "/Pfr/V2";
        }

        public Task<List<PfrDepartmentDto>> GetDepartmentListByRegionCodeAsync(string regionCode)
        {
            return GetAsync<List<PfrDepartmentDto>>("/GetDepartmentListByRegionCode", new {regionCode});
        }

        public Task<PfrDepartmentDto> GetDepartmentByCodeAsync(string code)
        {
            return GetAsync<PfrDepartmentDto>("/GetDepartmentByCode", new {code});
        }

        public Task<PfrRequisitesDto> GetRequisitesByRegionCodeAsync(string regionCode)
        {
            return GetAsync<PfrRequisitesDto>("/GetRequisitesByRegionCode", new {regionCode});
        }

        public Task<List<PfrDepartmentDto>> GetDepartmentsListByCodeTemplateAsync(string codeTemplate)
        {
            return GetAsync<List<PfrDepartmentDto>>("/GetDepartmentsListByCodeTemplate", new { codeTemplate });
        }
    }
}