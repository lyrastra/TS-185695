using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.SettlementAccounts
{
    internal interface ISettlementAccountsReader
    {
        Task<SettlementAccount> GetByIdAsync(int settlementAccountId);
    }
}
