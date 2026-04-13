using System;
using Moedelo.Accounting.Enums.PaymentOrder;
using Moedelo.Accounting.Enums.PaymentOrder.BudgetaryPayment;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.PaymentOrder
{
    public class BudgetaryPeriodDto
    {
        public BudgetaryPeriodDto()
        {
        }

        public BudgetaryPeriodDto(int number, BudgetaryPeriodType type, int year, DateTime? date = null)
        {
            Type = type;
            Number = number;
            Year = year;
        }

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