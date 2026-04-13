using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Kafka.Abstractions.Models
{
    public class BudgetaryPeriod
    {
        public BudgetaryPeriodType Type { get; set; }

        public int Number { get; set; }

        public int Year { get; set; }

        public DateTime? Date { get; set; }
    }
}
