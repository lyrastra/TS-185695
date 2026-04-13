using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.SettlementAccountEx;

namespace Moedelo.RequisitesV2.Client.SettlementAccountEx
{
    public interface ISettlementAccountExApiClient : IDI
    {
        Task<SettlementAccountExDto> GetBySettlementAccountIdAsync(int firmId, int userId, int settlementAccountId);

        Task SaveOrUpdateAsync(int firmId, int userId, SettlementAccountExSaveDto saveDto);
        
        Task DeleteBySettlementAccountIdAsync(int firmId, int userId, int settlementAccountId);
    }
}