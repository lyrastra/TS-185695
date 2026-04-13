using System.Threading.Tasks;

namespace Moedelo.Money.Business.SettlementAccounts
{
    internal interface ISettlementAccountsReader
    {
        Task<SettlementAccount> GetByIdAsync(int settlementAccountId);
    }
}
