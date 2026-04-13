using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.RequisitesV2.Dto.Funds;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.RequisitesV2.Client.Funds
{
    [InjectAsSingleton]
    public class PfrClient : BaseApiClient, IPfrClient
    {
        private readonly SettingValue apiEndPoint;
        
        public PfrClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<PfrDepartmentDto> GetDepartmentAsync(int firmId, int userId)
        {
            return GetAsync<PfrDepartmentDto>("/Pfr/Department", new { firmId, userId });
        }

        public Task SaveDepartmentAsync(int firmId, int userId, PfrDepartmentDto department)
        {
            return PostAsync($"/Pfr/Department?firmId={firmId}&userId={userId}", department);
        }

        public Task<PfrDto> GetPfrByIdAsync(int id)
        {
            return GetAsync<PfrDto>("/Pfr/GetPFRById", new {id});
        }

        public async Task<List<PfrDto>> GetPFRListByRegionAsync(int regionId)
        {
            var data = await GetAsync<ListWrapper<PfrDto>>("/Pfr/GetPFRListByRegion", new { regionId }).ConfigureAwait(false);
            return data.Items;
        }
    }
}