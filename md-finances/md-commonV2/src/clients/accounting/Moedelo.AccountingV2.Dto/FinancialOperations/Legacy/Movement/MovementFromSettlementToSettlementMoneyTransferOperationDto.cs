using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Movement
{
    public class MovementFromSettlementToSettlementMoneyTransferOperationDto : MovementOperationDto
    {
        public int? SettlementAccountFromId { get; set; }
        public int? SettlementAccountToId { get; set; }

        public override string Name => FinancialOperationNames.MovementFromSettlementToSettlementMoneyTransferOperation;
    }
}
