using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Snapshot
{
    public class BudgetaryPeriodDto
    {
        public DateTime? Date { get; set; }

        /// <summary> Тип периода: ГД, ПЛ, КВ, МС </summary>
        public BudgetaryPeriodType Type { get; set; }

        /// <summary>
        /// Номер в периоде: для МС — номер месяца, КВ — номер кваратала, ПЛ – номер полугодия, для ГД — 0.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Год платежа
        /// </summary>
        public int Year { get; set; }
    }
}
