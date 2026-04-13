using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class LoansThirdPartiesOutgoingOperationDto : OutgoingOperationDto
    {
        public string Recepient { get; set; }
        public double SumOfPercent { get; set; }
        public double SumOfLoan { get; set; }
        public string NumberOfConfirmDocument { get; set; }
        public decimal PatentSum { get; set; }

        public override string Name => FinancialOperationNames.LoansThirdPartiesOutgoingOperation;
    }
}
