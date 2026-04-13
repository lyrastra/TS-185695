using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Dto;
using Moedelo.AgentsV2.Dto.Enums;
using Moedelo.AgentsV2.Dto.Partners;
using Moedelo.AgentsV2.Dto.ResonalAccount;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AgentsV2.Client.PartnerAccount
{
    public interface IPartnerAccountApiClient : IDI
    {
        Task<bool> HasReferralLinkAsync(int partnerId, long? referralLink);

        Task<PartnerInfoDto> GetVipPartnerInfoAsync(string login);

        Task<ResponseStatusCode> ReplenishmentPartnerAccount(
            ReplenishmentPartnerAccountDto replenishmentPartnerAccountDto);

        Task IncrementTransitionsByReferalLinkForLeadCountAsync(
            TransitionByReferralLinkDto transitionByReferralLinkDto);
    }
}