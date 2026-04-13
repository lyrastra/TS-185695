using Moedelo.Money.Enums.Attributes;
using System.Reflection;

namespace Moedelo.Money.Enums.Extensions
{
    public static class OperationTypeExtensions
    {
        public static bool IsWorkerOperation(this OperationType type)
        {
            return type == OperationType.PaymentOrderOutgoingPaymentToNaturalPersons ||
                   type == OperationType.PaymentOrderOutgoingPaymentToAccountablePerson ||
                   type == OperationType.PaymentOrderIncomingRefundFromAccountablePerson;
        }

        public static bool IsOperationBetweenSettlementAccounts(this OperationType type)
        {
            return type == OperationType.PaymentOrderOutgoingTransferToAccount ||
                   type == OperationType.PaymentOrderIncomingTransferFromAccount;
        }

        public static bool IsBudgetaryOperation(this OperationType type)
        {
            return type == OperationType.BudgetaryPayment||
                   type == OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment;
        }

        public static OperationKind AsOperationKind(this OperationType operationType)
        {
            return GetOperationKind(operationType);
        }

        public static bool IsPaymentOrder(this OperationType operationType)
        {
            var operationKind = GetOperationKind(operationType);
            return operationKind == OperationKind.PaymentOrder;
        }

        public static bool IsCashOrder(this OperationType operationType)
        {
            var operationKind = GetOperationKind(operationType);
            return operationKind == OperationKind.CashOrder;
        }

        public static bool IsPurseOperation(this OperationType operationType)
        {
            var operationKind = GetOperationKind(operationType);
            return operationKind == OperationKind.PurseOperation;
        }

        private static OperationKind GetOperationKind(OperationType operationType)
        {
            return operationType.GetType()
                .GetCustomAttribute<OperationKindAttribute>()
                .Kind;
        }
    }
}
