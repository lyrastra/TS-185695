namespace Moedelo.Money.Business.Abstractions.CashOrders
{
    public interface ICashOrderAccessor
    {
        bool IsReadOnly(bool provideInAccounting);
    }
}
