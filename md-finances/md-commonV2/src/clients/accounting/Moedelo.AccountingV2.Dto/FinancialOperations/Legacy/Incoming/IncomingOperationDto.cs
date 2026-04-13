namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class IncomingOperationDto : MoneyTransferOperationDto
    {
        public long? KontragentSettlementAccountId { get; set; }
        public int? BillId { get; set; }
    }
}
