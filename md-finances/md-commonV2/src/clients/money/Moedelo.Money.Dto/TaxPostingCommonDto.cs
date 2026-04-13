using Moedelo.Common.Enums.Enums.TaxPostings;

namespace Moedelo.Money.Dto
{
    /// <summary>
    /// Проводка (УСН, ИП ОСНО, ООО ОСНО)
    /// </summary>
    public class TaxPostingCommonDto
    {
        /// <summary>
        /// Сумма (УСН, ИП ОСНО, ООО ОСНО)
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Источник дохода/расхода (ООО ОСНО)
        /// </summary>
        public OsnoTransferType? Type { get; set; }

        /// <summary>
        /// Вид дохода/расхода (ООО ОСНО)
        /// </summary>
        public OsnoTransferKind? Kind { get; set; }

        /// <summary>
        /// Тип нормируемого расхода (ООО ОСНО)
        /// </summary>
        public NormalizedCostType? NormalizedCostType { get; set; }

        /// <summary>
        /// Описание (УСН)
        /// </summary>
        public string Description { get; set; }
    }
}
