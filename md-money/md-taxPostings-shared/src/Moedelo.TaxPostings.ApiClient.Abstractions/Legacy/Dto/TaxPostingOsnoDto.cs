using Moedelo.TaxPostings.Enums;
using System;

namespace Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto
{
    public class TaxPostingOsnoDto
    {
        public long Id { get; set; }

        public long DocumentId { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

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
