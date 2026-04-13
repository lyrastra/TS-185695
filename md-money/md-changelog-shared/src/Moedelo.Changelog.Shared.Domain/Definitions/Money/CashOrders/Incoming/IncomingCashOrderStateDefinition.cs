namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Incoming
{
    public abstract class IncomingCashOrderStateDefinition<TDefinition, TState>
        : CashOrderStateDefinition<TDefinition, TState>
        where TDefinition : AutoEntityStateDefinition<TDefinition, TState>, new()
    {
        protected IncomingCashOrderStateDefinition()
        {
            AddTag("incoming_cash_order");
        }
    }
}
