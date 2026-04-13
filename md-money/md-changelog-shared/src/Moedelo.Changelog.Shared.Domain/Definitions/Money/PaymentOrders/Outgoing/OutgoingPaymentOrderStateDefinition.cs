namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing
{
    public abstract class OutgoingPaymentOrderStateDefinition<TDefinition, TState>
        : PaymentOrderStateDefinition<TDefinition, TState>
        where TDefinition : AutoEntityStateDefinition<TDefinition, TState>, new()
    {
        protected OutgoingPaymentOrderStateDefinition()
        {
            AddTag("outgoing_payment_order");
        }
    }
}
