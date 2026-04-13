using System.Threading.Tasks;
using Moedelo.AgentsV2.Client.Dto.Lead;
using Moedelo.AgentsV2.Dto.Leads;
using Moedelo.Common.Enums.Enums.Agents;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AgentsV2.Client.LeadClient
{
    [InjectAsSingleton]
    public class LeadApiClient : BaseApiClient, ILeadApiClient
    {
        private readonly SettingValue apiEndPoint;

        public LeadApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AgentsApiUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<LeadStatusInfoDto> GetLeadStatusInfo(string loginQuery)
        {
            var result = await GetAsync<DataWrapper<LeadStatusInfoDto>>("/Lead/GetLeadStatusInfo", new { loginQuery }).ConfigureAwait(false);
            return result.Data;
        }

        public Task SetLeadStatus(int userId, LeadStatus status)
        {
            return PostAsync($"/Lead/SetLeadStatus?userId={userId}&status={status}");
        }

        public Task AddNewPartnerLeadAsync(AddNewPartnerLeadDto dto)
        {
            return PostAsync<AddNewPartnerLeadDto>($"/Lead/AddNewPartnerLead", dto);
        }

        public Task<PartnerLeadsResponseDto> GetPartnerLeadsAsync(PartnerLeadRequestDto dto)
        {
            return GetAsync<PartnerLeadsResponseDto>("/Lead/GetPartnerLeads", dto);
        }
    }
}
