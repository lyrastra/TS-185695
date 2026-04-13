using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Upds.Dto
{
    public class PurchasesUpdItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Count { get; set; }

        public NdsType NdsType { get; set; }

        public decimal NdsSum { get; set; }

        public decimal SumWithoutNds { get; set; }

        public decimal SumWithNds { get; set; }

        /// <summary>
        /// Рассчитанные автоматически суммы отредактированы пользователем
        /// </summary>
        public bool IsCustomSums { get; set; }

        public long? StockProductId { get; set; }

        public ItemType ItemType { get; set; }

        public decimal Price { get; set; }

        public string Unit { get; set; }

        public int? NdsDeclarationSection7CodeId { get; set; }
        
        /// <summary> Счет расхода во входящем УПД с типом услуга </summary>
        public CostSyntheticAccountCode? ActivityAccountCode { get; set; }
        
        /// <summary> 
        /// Код вида операции, если тип УПД = 1 (УПД является также сч-ф) 
        /// </summary>
        public int? NdsOperationType { get; set; }
        
        /// <summary>
        /// Страна импортируемого товара
        /// </summary>
        public string GtdCountry { get; set; }
        
        /// <summary>
        /// ГТД импортируемого товара
        /// </summary>
        public string GtdNumber { get; set; }
    }
}