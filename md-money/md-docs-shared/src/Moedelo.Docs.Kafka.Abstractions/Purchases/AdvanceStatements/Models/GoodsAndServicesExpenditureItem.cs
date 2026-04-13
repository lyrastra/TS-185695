using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Models
{
    /// <summary>
    /// Позиция авансового отчёта с типом "Товары, материалы, услуги"
    /// </summary>
    public class GoodsAndServicesExpenditureItem
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Сумма, прининятая в отчёте
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// Тип НДС
        /// </summary>
        public NdsType NdsType { get; set; }

        /// <summary>
        /// Идентификатор товара на складе
        /// </summary>
        public long? StockProductId { get; set; }

        /// <summary>
        /// Тип СНО
        /// </summary>
        public TaxationSystemType TaxationSystemType { get; set; }
    }
}