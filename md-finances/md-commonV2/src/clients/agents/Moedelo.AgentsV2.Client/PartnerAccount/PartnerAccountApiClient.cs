using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Dto;
using Moedelo.AgentsV2.Dto.Enums;
using Moedelo.AgentsV2.Dto.Partners;
using Moedelo.AgentsV2.Dto.ResonalAccount;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AgentsV2.Client.PartnerAccount
{
    [InjectAsSingleton]
    public class PartnerAccountApiClient: BaseApiClient, IPartnerAccountApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PartnerAccountApiClient(
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
        
        public Task<bool> HasReferralLinkAsync(int partnerId, long? referralLink)
        {
            return GetAsync<bool>($"/PartnerAccountV2/HasReferralLink?partnerId={partnerId}&referralLink={referralLink}");
        }

        public async Task<PartnerInfoDto> GetVipPartnerInfoAsync(string login)
        {
            var result = await GetAsync<DataWrapper<PartnerInfoDto>>("/PartnerAccount/GetVipPartnerInfo", new { login }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<ResponseStatusCode> ReplenishmentPartnerAccount(ReplenishmentPartnerAccountDto replenishmentPartnerAccountDto)
        {
            var result = await PostAsync<ReplenishmentPartnerAccountDto, StatusWrapper<ResponseStatusCode>>("/PartnerAccount/ReplenishmentPartnerAccount", replenishmentPartnerAccountDto).ConfigureAwait(false);
            return result.StatusCode;
        }

        public Task IncrementTransitionsByReferalLinkForLeadCountAsync(
            TransitionByReferralLinkDto transitionByReferralLinkDto)
        {
            return PostAsync<TransitionByReferralLinkDto>(
                "/PartnerAccount/IncrementTransitionsByReferalLinkForLeadCount",
                transitionByReferralLinkDto);
        }
    }
}