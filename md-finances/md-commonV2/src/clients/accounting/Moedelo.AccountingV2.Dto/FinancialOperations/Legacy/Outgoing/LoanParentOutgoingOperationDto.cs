using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class LoanParentOutgoingOperationDto : OutgoingOperationDto
    {
        public string NumberOfConfirmDocument { get; set; }
        public double SumOfPercent { get; set; }
        public double SumOfLoan { get; set; }

        public override string Name => FinancialOperationNames.LoanParentOutgoingOperation;
    }
}
