using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Business.PaymentOrders.Providing
{
    internal static class PaymentOrdersProvidingExtensions
    {
        public static IConcretePaymentOrderProvider GetProvider(
            this IReadOnlyDictionary<OperationType, IConcretePaymentOrderProvider> providers,
            OperationType operationType)
        {
            if (providers.TryGetValue(operationType, out var provider))
            {
                return provider;
            }
            // BP-8683 Обращение к старому бэкенду удалено окончательно.
            // Важно! Перепроведение реализовано не для всех операций.
            // Реализованы операции, связанные с другими документами (и перепроводящиеся через них),
            // либо самые часто используемые, либо те, для которых проведение было реализовано изначально.
            // Для остальных перепроведение автоматически не вызывается.
            // Если появится необходимость перепровести нереализованные операции вручную через апи,
            // то вам придеться реализовать для них проведение на событиях самостоятельно..
            throw new NotImplementedException($"Implementation of IConcretePaymentOrderProvider for operation type {operationType} ({(int)operationType}) is not found");
        }
    }
}
