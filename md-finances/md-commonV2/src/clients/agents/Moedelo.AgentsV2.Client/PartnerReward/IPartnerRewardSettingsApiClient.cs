using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Client.Dto.Partner;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AgentsV2.Client.PartnerReward
{
    public interface IPartnerRewardSettingsApiClient : IDI
    {
        Task<PartnerRewardSettingsDto> GetByPartnerIdAsync(int partnerId);

        Task SaveAsync(PartnerRewardSettingsDto dto);
    }
}