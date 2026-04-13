namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming
{
    public abstract class IncomingPaymentOrderStateDefinition<TDefinition, TState>
        : PaymentOrderStateDefinition<TDefinition, TState>
        where TDefinition : AutoEntityStateDefinition<TDefinition, TState>, new()
    {
        protected IncomingPaymentOrderStateDefinition()
        {
            AddTag("incoming_payment_order");
        }
    }
}
