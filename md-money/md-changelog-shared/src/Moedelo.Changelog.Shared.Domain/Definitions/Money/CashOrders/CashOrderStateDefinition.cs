namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders
{
    public abstract class CashOrderStateDefinition<TDefinition, TState>
        : MoneyStateDefinition<TDefinition, TState>
        where TDefinition : AutoEntityStateDefinition<TDefinition, TState>, new()
    {
        protected CashOrderStateDefinition()
        {
            AddTag("cash_order");
        }
    }
}
