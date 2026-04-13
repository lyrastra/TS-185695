
using Moedelo.Finances.Domain.SettlementAccounts;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.Business.SettlementAccounts
{
    public interface ISettlementAccountsReader : IDI
    {
        Task<SettlementAccount> GetSettlementAccount(int firmId, int userId, int Id);
        Task<List<SettlementAccount>> GetSettlementAccounts(int firmId, int userId, List<long> subcontoIds, long? sourceId);
    }
}
