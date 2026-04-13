using System;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class LoanParentOperationDto : IncomingOperationDto
    {
        public string Recipient { get; set; }
        public DateTime? ProjectDate { get; set; }
        public string ProjectNumber { get; set; }
        public DateTime? DateOver { get; set; }

        public override string Name => FinancialOperationNames.LoanParentOperation;
    }
}
