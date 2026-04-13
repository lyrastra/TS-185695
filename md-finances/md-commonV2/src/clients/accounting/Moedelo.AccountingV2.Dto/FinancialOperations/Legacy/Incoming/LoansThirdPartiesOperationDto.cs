using System;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class LoansThirdPartiesOperationDto : IncomingOperationDto
    {
        public string FioThirdParties { get; set; }
        public DateTime? DateOver { get; set; }
        public string ProjectNumber { get; set; }
        public DateTime? ProjectDate { get; set; }

        public override string Name => FinancialOperationNames.LoansThirdPartiesOperation;
    }
}
