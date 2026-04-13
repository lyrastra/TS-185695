using System.Collections.Generic;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class CashIncomingOperationDto : IncomingOperationDto
    {
        public decimal SaleSum { get; set; }
        public IReadOnlyCollection<PatentInMoneyDto> Patents { get; set; }

        public override string Name => FinancialOperationNames.CashIncomingOperation;
    }
}
