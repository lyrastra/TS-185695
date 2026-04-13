namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders
{
    public abstract class PaymentOrderStateDefinition<TDefinition, TState>
        : MoneyStateDefinition<TDefinition, TState>
        where TDefinition : AutoEntityStateDefinition<TDefinition, TState>, new()
    {
        protected PaymentOrderStateDefinition()
        {
            AddTag("payment_order");
        }
    }
}
