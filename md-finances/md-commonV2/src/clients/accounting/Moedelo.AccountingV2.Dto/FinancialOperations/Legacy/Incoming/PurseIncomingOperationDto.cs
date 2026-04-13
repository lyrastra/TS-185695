using System.Collections.Generic;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class PurseIncomingOperationDto : IncomingOperationDto
    {
        public IReadOnlyCollection<PatentInMoneyDto> Patents { get; set; }

        public override string Name => FinancialOperationNames.PurseIncomingOperation;
    }
}
