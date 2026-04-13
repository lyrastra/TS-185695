namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing
{
    public abstract class OutgoingCashOrderStateDefinition<TDefinition, TState>
        : CashOrderStateDefinition<TDefinition, TState>
        where TDefinition : AutoEntityStateDefinition<TDefinition, TState>, new()
    {
        protected OutgoingCashOrderStateDefinition()
        {
            AddTag("outgoing_cash_order");
        }
    }
}
