namespace Moedelo.BankIntegrations.Models.PaymentOrder
{
    public class PaymentOrderWithBaseId: PaymentOrder
    {
        public long DocumentBaseId { get; set; }
    }
}