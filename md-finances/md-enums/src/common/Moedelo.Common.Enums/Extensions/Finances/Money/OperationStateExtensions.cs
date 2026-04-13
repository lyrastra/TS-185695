using System.Linq;
using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Common.Enums.Extensions.Finances.Money
{
    public static class OperationStateExtensions
    {
        // поправил - обнови enums в проектах, где он используется
        public static OperationState[] BadOperationStates => new[]
        {
            OperationState.Duplicate,
            OperationState.MissingKontragent,
            OperationState.MissingWorker,
            OperationState.ImportProcessing,
            OperationState.DuplicateProcessing,
            OperationState.MissingKontragentProcessing,
            OperationState.MissingWorkerProcessing,
            OperationState.Invalid,
            OperationState.MissingExchangeRate,
            OperationState.MissingCurrencySettlementAccount,
            OperationState.MissingContract,
            OperationState.MissingCommissionAgent,
            OperationState.NoSubPayments,
            OperationState.AmbiguousOperationType,
            OperationState.OutsourceProcessingValidationFailed
        };

        public static bool IsBadOperationState(this OperationState operationState)
        {
            return BadOperationStates.Contains(operationState);
        }
    }
}
