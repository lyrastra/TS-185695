using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Movement
{
    public class MovementFromPurseToSettlementMoneyTransferOperationDto : MovementOperationDto
    {
        public override string Name => FinancialOperationNames.MovementFromPurseToSettlementMoneyTransferOperation;
    }
}
