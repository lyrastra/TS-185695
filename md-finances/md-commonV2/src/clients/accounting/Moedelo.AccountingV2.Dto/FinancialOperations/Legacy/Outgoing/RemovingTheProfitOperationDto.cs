using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class RemovingTheProfitOperationDto : OutgoingOperationDto
    {
        public override string Name => FinancialOperationNames.RemovingTheProfitOperation;
    }
}
