namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount
{
    public class TransferToAccountSaveResponse : PaymentOrderSaveResponse
    {
        public long TransferFromAccountBaseId { get; set; }
    }
}