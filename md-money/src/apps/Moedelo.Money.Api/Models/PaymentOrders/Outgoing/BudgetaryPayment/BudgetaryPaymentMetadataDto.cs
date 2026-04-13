namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPaymentMetadataDto
    {
        public BudgetaryAccountDto[] Accounts { get; set; }
        public BudgetaryPaymentReasonDto[] PaymentReasons { get; set; }
        public BudgetaryPaymentReasonDto[] PaymentSubReasons { get; set; }
        public BudgetaryStatusOfPayerDto[] StatusOfPayers { get; set; }
    }
}
