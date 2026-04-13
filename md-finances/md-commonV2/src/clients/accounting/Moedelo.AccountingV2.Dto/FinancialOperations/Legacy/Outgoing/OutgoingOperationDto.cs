namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class OutgoingOperationDto : MoneyTransferOperationDto
    {
        public string BillNumber { get; set; }
        public long? KontragentSettlementAccountId { get; set; }
    }
}
