using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class OtherOutgoingOperationDto : OutgoingOperationDto
    {
        public int? WorkerId { get; set; }
        public OtherTransferType OtherType { get; set; }
        public int Amount { get; set; }
        public decimal PatentSum { get; set; }

        public override string Name => FinancialOperationNames.OtherOutgoingOperation;
    }
}
