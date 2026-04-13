using System;

namespace Moedelo.Accounting.Domain.Shared.NdsRates
{
    public static class NdsRateStartDates
    {
        public static readonly DateTime Nds20StartDate = new DateTime(2019, 1, 1);

        public static readonly DateTime Nds5Or7StartDate = new DateTime(2025, 1, 1);

        public static readonly DateTime Nds22StartDate = new DateTime(2026, 1, 1);

        public static readonly DateTime CashAndPurseNdsStartDate = new DateTime(2025, 1, 1);
    }
}