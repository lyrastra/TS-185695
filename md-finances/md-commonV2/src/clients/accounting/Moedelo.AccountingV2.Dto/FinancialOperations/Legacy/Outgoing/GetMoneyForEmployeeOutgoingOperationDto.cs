using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class GetMoneyForEmployeeOutgoingOperationDto : OutgoingOperationDto
    {
        public int? WorkerId { get; set; }
        public decimal PatentSum { get; set; }
        public int AdvanceStatementId { get; set; }

        public override string Name => FinancialOperationNames.GetMoneyForEmployeeOperation;
    }
}
