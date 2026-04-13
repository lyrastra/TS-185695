using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy
{
    public interface ICommissionAgentsApiClient
    {
        Task<CreateCommissionByInnResultDto> CreateCommissionAgentAsync(FirmId firmId, UserId userId, string inn);
    }
}