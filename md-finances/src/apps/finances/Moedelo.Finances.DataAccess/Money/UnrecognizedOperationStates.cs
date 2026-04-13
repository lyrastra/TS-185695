using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.DataAccess.Money
{
    internal static class UnrecognizedOperationStates
    {
        private static OperationState[] OperationStateList = new[]
        {
            OperationState.Duplicate,
            OperationState.MissingKontragent,
            OperationState.MissingWorker,
            OperationState.MissingExchangeRate,
            OperationState.MissingCurrencySettlementAccount,
            OperationState.MissingContract,
            OperationState.MissingCommissionAgent,
            OperationState.NoSubPayments,
            OperationState.AmbiguousOperationType,
            OperationState.OutsourceProcessingValidationFailed
        };

        public static OperationState[] List => OperationStateList; 
    }
}
