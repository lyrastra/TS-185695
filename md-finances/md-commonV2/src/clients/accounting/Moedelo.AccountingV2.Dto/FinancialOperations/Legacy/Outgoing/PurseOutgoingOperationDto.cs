using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class PurseOutgoingOperationDto : OutgoingOperationDto
    {
        public override string Name => FinancialOperationNames.PurseOutgoingOperation;
    }
}
