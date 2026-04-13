using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface ISettlementAccountExApiClient
    {
        Task<SettlementAccountExDto> GetBySettlementAccountIdAsync(FirmId firmId, UserId userId, int settlementAccountId);
    }
}