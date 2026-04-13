using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class ContributionOfOwnFundsOperationDto : IncomingOperationDto
    {
        public override string Name => FinancialOperationNames.ContributionOfOwnFundsOperation;
    }
}
