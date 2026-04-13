namespace Moedelo.AccountingV2.Dto.PaymentDocuments
{
    public class ConfirmingOperationDto
    {
        public long FinancialOperationBaseId { get; set; }

        public decimal Sum { get; set; }
    }
}
