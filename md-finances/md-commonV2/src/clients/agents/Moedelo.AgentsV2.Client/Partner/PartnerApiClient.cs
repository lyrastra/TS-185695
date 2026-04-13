using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Client.Dto.Partner;
using Moedelo.AgentsV2.Dto;
using Moedelo.AgentsV2.Dto.Leads;
using Moedelo.AgentsV2.Dto.Partners;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AgentsV2.Client.Partner
{
    [InjectAsSingleton]
    public class PartnerApiClient : BaseApiClient, IPartnerApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PartnerApiClient(
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
        
        public Task<UserRegistrationDto> CreateWebmasterAsync(PartnerDto dto)
        {
            return PostAsync<PartnerDto, UserRegistrationDto>("/Partners/CreateWebmaster", dto);
        }

        public Task<UserRegistrationDto> CreateTrialAgentAsync(PartnerDto dto)
        {
            return PostAsync<PartnerDto, UserRegistrationDto>("/Partners/CreateTrialAgent", dto);
        }

        public Task<UserRegistrationDto> CreateAgentAsync(PartnerDto dto)
        {
            return PostAsync<PartnerDto, UserRegistrationDto>("/Partners/CreateAgent", dto);
        }

        public Task AddMoneyAsync(PartnerMoneyDto dto)
        {
            return PostAsync("/Partners/AddMoney", dto);
        }

        public async Task<PartnerInfoResponseDto> GetPartnerAsync(int partnerId)
        {
            var result = await GetAsync<DataWrapper<PartnerInfoResponseDto>>("/Partners/GetPartner", new { partnerId }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<List<PartnerInfoDto>> GetPartnersAsync(int count, int page)
        {
            var result = await GetAsync<ListWrapper<PartnerInfoDto>>("/Partners/GetPartners", new { count, page }).ConfigureAwait(false);
            return result.Items;
        }

        public Task<ListWithTotalDto<PartnerInfoDto>> GetPartnersWithTotalAsync(int count, int page)
        {
            return GetAsync<ListWithTotalDto<PartnerInfoDto>>("/Partners/GetPartners", new { count, page });
        }

        public async Task<PartnerInfoDto> GetPartnerInfo(int? partnerId, string login)
        {
            var result = await GetAsync<DataWrapper<PartnerInfoDto>>("/Partners/GetPartnerInfo", new { partnerId, login }).ConfigureAwait(false);
            return result.Data;
        }

        public Task ProvidePaymentsToPartnersForLeadFirstPayment(PartnerLeadPaymentDto dto)
        {
            return PostAsync<PartnerLeadPaymentDto>("/Partners/ProvidePaymentsToPartnersForLeadFirstPayment", dto);
        }

        public Task ProvidePaymentsToPartnersForLeadPayment(PartnerLeadPaymentDto dto)
        {
            return PostAsync<PartnerLeadPaymentDto>("/Partners/ProvidePaymentsToPartnersForLeadPayment", dto);
        }

        public async Task<bool> PartnerIsBannedAsync(string login)
        {
            var result = await GetAsync<DataWrapper<bool>>("/Partners/PartnerIsBanned", new { login })
                .ConfigureAwait(false);
            
            return result.Data;
        }
    }
}