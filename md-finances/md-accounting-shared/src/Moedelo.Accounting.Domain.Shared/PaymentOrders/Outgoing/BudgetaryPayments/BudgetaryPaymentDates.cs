using System;

namespace Moedelo.Accounting.Domain.Shared.PaymentOrders.Outgoing.BudgetaryPayments
{
    public static class BudgetaryPaymentDates
    {
        // Приказ Минфина РФ от 16.05.2025 № 58н
        // правила заполнения платежных поручений при перечислении налогов, сборов и страховых взносов в бюджет, как в составе ЕНП, так и по прочим налогам и сборам
        public static DateTime FormatDate16052025 = new DateTime(2026, 4, 1);
    }
}
