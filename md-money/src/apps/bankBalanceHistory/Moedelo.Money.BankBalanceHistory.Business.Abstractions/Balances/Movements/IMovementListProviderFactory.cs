using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances.Enums;

namespace Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances.Movements
{
    public interface IMovementListProviderFactory
    {
        IMovementListProvider GetProvider(MovementListSourceType type);
    }
}
