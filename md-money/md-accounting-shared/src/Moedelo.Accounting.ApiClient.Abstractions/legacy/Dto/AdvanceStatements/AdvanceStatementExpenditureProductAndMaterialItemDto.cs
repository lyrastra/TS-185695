using Moedelo.Accounting.Enums;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.AdvanceStatements
{
    public class AdvanceStatementExpenditureProductAndMaterialItemDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Unit { get; set; }

        public decimal Count { get; set; }

        public decimal SumWithNds { get; set; }

        public decimal NdsSum { get; set; }

        public int NdsType { get; set; }

        public long? StockProductId { get; set; }

        /// <summary>
        /// Учесть в СНО
        /// </summary>
        public virtual TaxationSystemType? TaxationSystem { get; set; }
    }
}
