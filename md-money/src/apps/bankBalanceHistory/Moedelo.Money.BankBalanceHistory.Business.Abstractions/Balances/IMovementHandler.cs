using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances
{
    public interface IMovementHandler
    {
        Task ProcessMovementAsync(
            int firmId,
            string fileId,
            MovementListSourceType sourceType);
    }
}
