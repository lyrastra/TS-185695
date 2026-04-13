using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.BankBalanceHistory.Domain;

namespace Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances
{
    public interface IBalanceUpdater
    {
        Task UpdateAsync(IReadOnlyCollection<BankBalanceUpdateRequest> requests);
    }
}