using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.Partner;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BackofficeV2.Client.Partner
{
    public interface IPartnerClient : IDI
    {
        Task UpdateBankLeadFixationAsync();
        Task<PartnerNameAndSelfMaintainedResponseDto> GetPartnerNameByWorkerIdAsync(int partnerId);
        Task AttemptToSetFixationsAsync(AttemptToSetFixationsDto dto);
        Task<PartnerEmployeeStatisticResponse> GetPartnerEmployeeStatisticAsync(int partnerUserId, int partnerFirmId);
        Task<string> GenerateReferalUtmSourceByPriceListIdAsync(
            int referralId,
            int priceListId,
            bool isReferralLink = false);

        Task<string> GenerateReferalUtmSourceByTariffIdAsync(
            int referralId,
            int tariffId,
            bool isReferralLink = false);
    }
}