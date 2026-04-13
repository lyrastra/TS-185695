using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    public class ProductAndMaterialSubItemClientData : INdsDocumentClientData
    {
        public string Name { get; set; }

        public string Unit { get; set; }

        public decimal Count { get; set; }

        public decimal Price { get; set; }

        public NdsType NdsType { get; set; }

        public string UnitCode { get; set; }

        public long? StockProductId { get; set; }

        public bool UseNds { get; set; }

        public decimal SumWithoutNds { get; set; }

        public decimal NdsSum { get; set; }

        public decimal SumWithNds { get; set; }

        public bool IsCustomSums { get; set; }

        public int ActivityAccountCode { get; set; }

        public StockProductTypeEnum Type { get; set; }

        /// <summary> Субконто Id отдела</summary>
        public long? DivisionSubcontoId { get; set; }

        public decimal Sum { get; set; }

        /// <summary>
        /// Учесть в ОСНО/УСН
        /// </summary>
        public decimal? TaxableSum { get; set; }

        /// <summary>
        /// Учесть в (переключатель СНО)
        /// </summary>
        public TaxationSystemType TaxationSystemType { get; set; }
    }
}