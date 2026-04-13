using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.Partner;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BackofficeV2.Client.Partner
{
    [InjectAsSingleton]
    public class PartnerClient : BaseApiClient, IPartnerClient
    {
        private readonly SettingValue apiEndPoint;

        public PartnerClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("BackOfficePrivateApiEndpoint");
        }

        public Task UpdateBankLeadFixationAsync()
        {
            return PostAsync("/Rest/Partner/UpdateBankLeadFixation");
        }

        public Task<PartnerNameAndSelfMaintainedResponseDto> GetPartnerNameByWorkerIdAsync(int partnerId)
        {
            return GetAsync<PartnerNameAndSelfMaintainedResponseDto>("/Rest/Partner/GetPartnerNameByWorkerId", new { userId = partnerId });
        }

        public Task AttemptToSetFixationsAsync(AttemptToSetFixationsDto dto)
        {
            return PostAsync("/Rest/Partner/V2/AttemptToSetFixations", dto);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<PartnerEmployeeStatisticResponse> GetPartnerEmployeeStatisticAsync(int partnerUserId, int partnerFirmId)
        {
            return GetAsync<PartnerEmployeeStatisticResponse>("/Rest/Partner/V2/PartnerEmployeeStatistic", new { partnerUserId, partnerFirmId });
        }

        public Task<string> GenerateReferalUtmSourceByPriceListIdAsync(
            int referralId, 
            int priceListId,
            bool isReferralLink = false)
        {
            return GetAsync<string>(
                "/Rest/Partner/V2/GenerateReferalUtmSourceByPriceListId", 
                new { referralId, priceListId, isReferralLink }
            );
        }

        public Task<string> GenerateReferalUtmSourceByTariffIdAsync(
            int referralId,
            int tariffId,
            bool isReferralLink = false)
        {
            return GetAsync<string>(
                "/Rest/Partner/V2/GenerateReferalUtmSourceByTariffId",
                new { referralId, tariffId, isReferralLink }
            );
        }
    }
}