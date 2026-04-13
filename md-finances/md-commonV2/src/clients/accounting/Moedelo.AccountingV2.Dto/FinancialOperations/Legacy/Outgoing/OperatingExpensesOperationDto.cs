using System;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class OperatingExpensesOperationDto : OutgoingOperationDto
    {
        public DateTime? DateOfConfirmDocument { get; set; }
        public string NumberOfConfirmDocument { get; set; }
        public double SumOfConfirmDocument { get; set; }
        public string ConfirmDocumentType { get; set; }
        public long AdvanceStatementId { get; set; }
        public int? WorkerId { get; set; }
        public decimal PatentSum { get; set; }
        public override string Name => FinancialOperationNames.OperatingExpensesOperation;
    }
}
