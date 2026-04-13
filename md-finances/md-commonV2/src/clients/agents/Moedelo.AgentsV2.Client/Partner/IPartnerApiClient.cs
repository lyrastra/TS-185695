using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Client.Dto.Partner;
using Moedelo.AgentsV2.Dto;
using Moedelo.AgentsV2.Dto.Leads;
using Moedelo.AgentsV2.Dto.Partners;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AgentsV2.Client.Partner
{
    public interface IPartnerApiClient : IDI
    {
        Task<UserRegistrationDto> CreateWebmasterAsync(PartnerDto dto);

        Task<UserRegistrationDto> CreateTrialAgentAsync(PartnerDto dto);

        Task<UserRegistrationDto> CreateAgentAsync(PartnerDto dto);

        Task AddMoneyAsync(PartnerMoneyDto dto);

        Task<PartnerInfoResponseDto> GetPartnerAsync(int partnerId);

        Task<List<PartnerInfoDto>> GetPartnersAsync(int count, int page);
        Task<ListWithTotalDto<PartnerInfoDto>> GetPartnersWithTotalAsync(int count, int page);

        Task<PartnerInfoDto> GetPartnerInfo(int? partnerId, string login);

        Task ProvidePaymentsToPartnersForLeadFirstPayment(PartnerLeadPaymentDto dto);

        Task ProvidePaymentsToPartnersForLeadPayment(PartnerLeadPaymentDto dto);

        Task<bool> PartnerIsBannedAsync(string login);
    }
}