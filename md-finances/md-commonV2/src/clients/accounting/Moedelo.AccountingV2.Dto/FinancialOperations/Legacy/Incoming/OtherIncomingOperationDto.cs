using System.Collections.Generic;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class OtherIncomingOperationDto : IncomingOperationDto
    {
        public int? WorkerId { get; set; }
        public OtherTransferType OtherType { get; set; }
        public decimal PercentSum { get; set; }
        public IReadOnlyCollection<PatentInMoneyDto> Patents { get; set; }

        public override string Name => FinancialOperationNames.OtherIncomingOperation;
    }
}
