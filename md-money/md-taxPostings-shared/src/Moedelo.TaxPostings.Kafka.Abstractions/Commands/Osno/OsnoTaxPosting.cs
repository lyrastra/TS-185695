using Moedelo.TaxPostings.Enums;
using System;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Commands.Osno
{
    public class OsnoTaxPosting
    {
        /// <summary>
        /// Дата проводки
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма проводки
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Направление движения денег (приход/расход)
        /// </summary>
        public TaxPostingDirection Direction { get; set; }

        /// <summary>
        /// Источник дохода/расхода (только для ОСНО)
        /// </summary>
        public OsnoTransferType Type { get; set; }

        /// <summary>
        /// Вид дохода/расхода (только для ОСНО)
        /// </summary>
        public OsnoTransferKind Kind { get; set; }

        /// <summary>
        /// Тип нормируемого расхода (только для ОСНО)
        /// </summary>
        public NormalizedCostType NormalizedCostType { get; set; }
    }
}
