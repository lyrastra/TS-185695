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
    public class FssClient : BaseApiClient, IFssClient
    {
        private readonly SettingValue apiEndPoint;
        
        public FssClient(
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

        public Task<FssDepartmentDto> GetDepartmentAsync(int firmId, int userId)
        {
            return GetAsync<FssDepartmentDto>("/Fss/Department", new { firmId, userId });
        }

        public Task SaveDepartmentAsync(int firmId, int userId, FssDepartmentDto department)
        {
            return PostAsync($"/Fss/Department?firmId={firmId}&userId={userId}", department);
        }

        public Task<List<FssRateDto>> GetRatesAsync(int firmId, int userId)
        {
            return GetAsync<List<FssRateDto>>("/Fss/Rate", new { firmId, userId });
        }

        public Task<FssRateDto> GetRateAsync(int firmId, int userId, int year)
        {
            return GetAsync<FssRateDto>($"/Fss/Rate/{year}", new { firmId, userId });
        }

        public Task SaveRatesAsync(int firmId, int userId, List<FssRateDto> rates)
        {
            return PostAsync($"/Fss/Rate?firmId={firmId}&userId={userId}", rates);
        }
    }
}