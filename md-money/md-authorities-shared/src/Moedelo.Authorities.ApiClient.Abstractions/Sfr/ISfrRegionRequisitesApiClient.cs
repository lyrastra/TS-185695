using Moedelo.Authorities.ApiClient.Abstractions.Sfr.Dto;
using System.Threading.Tasks;

namespace Moedelo.Authorities.ApiClient.Abstractions.Sfr
{
    public interface ISfrRegionRequisitesApiClient
    {
        Task<SfrRegionRequisitesDto> GetByRegionCodeAsync(string regionCode);
        Task<bool> MatchAsync(string settlementAccount, string unifiedSettlementAccount);
    }
}
