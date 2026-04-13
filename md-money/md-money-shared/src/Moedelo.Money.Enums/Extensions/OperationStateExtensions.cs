using System.Collections.Generic;

namespace Moedelo.Money.Enums.Extensions
{
    /// <summary>
    /// ВАЖНО: синхронизировать с https://github.com/moedelo/md-enums/blob/master/src/common/Moedelo.Common.Enums/Extensions/Finances/Money/OperationStateExtensions.cs
    /// </summary>
    public static class OperationStateExtensions
    {
        // поправил - обнови enums в проектах, где он используется
        public static ISet<OperationState> BadOperationStates => new HashSet<OperationState>
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
