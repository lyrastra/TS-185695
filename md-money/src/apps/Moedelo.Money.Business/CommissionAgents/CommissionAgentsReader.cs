using Moedelo.CommissionAgents.ApiClient.Abstractions;
using Moedelo.CommissionAgents.ApiClient.Abstractions.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CommissionAgents
{
    [InjectAsSingleton(typeof(CommissionAgentsReader))]
    internal sealed class CommissionAgentsReader
    {
        private readonly ICommissionAgentsApiClient commissionAgentsApiClient;

        public CommissionAgentsReader(
            ICommissionAgentsApiClient commissionAgentsApiClient)
        {
            this.commissionAgentsApiClient = commissionAgentsApiClient;
        }

        public async Task<CommissionAgent> GetByKontragentIdAsync(int kontragentId)
        {
            var dto = await commissionAgentsApiClient.GetByKontragentIdAsync(kontragentId);
            return dto != null
                ? Map(dto)
                : null;
        }

        private static CommissionAgent Map(CommissionAgentDto dto)
        {
            return new CommissionAgent
            {
                Id = dto.Id,
                Inn = dto.Inn,
                KontragentId = dto.KontragentId
            };
        }

        public async Task<bool> HasAccessAsync()
        {
            var result = await commissionAgentsApiClient.HasAccessAsync();
            return result.HasAccessToMarketplacesAndCommissionAgents;
        }
    }
}
