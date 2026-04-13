namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.TransferToAccount
{
    public class TransferToAccountSaveResponseDto : PaymentOrderSaveResponseDto
    {
        public long TransferFromAccountBaseId { get; set; }
    }
}
