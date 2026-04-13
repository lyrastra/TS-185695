using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class BankFeeOutgoingOperationDto : OutgoingOperationDto
    {
        public string MemorialOrderNumber { get; set; }
        public decimal PatentSum { get; set; }

        public override string Name => FinancialOperationNames.BankFeeOutgoingOperation;
    }
}
