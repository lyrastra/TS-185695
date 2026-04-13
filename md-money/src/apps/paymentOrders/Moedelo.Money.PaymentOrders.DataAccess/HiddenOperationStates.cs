using System.Collections.Generic;
using Moedelo.Money.Enums;

namespace Moedelo.Money.PaymentOrders.DataAccess
{
    internal static class HiddenOperationStates
    {
        public static IReadOnlyCollection<OperationState> All => new[]
        {
            OperationState.ImportProcessing,
            OperationState.DuplicateProcessing,
            OperationState.MissingKontragentProcessing,
            OperationState.MissingWorkerProcessing,
            OperationState.Invalid
        };
    }
}