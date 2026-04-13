using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Domain.TaxPostings
{
    public class OsnoTaxPosting
    {
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
