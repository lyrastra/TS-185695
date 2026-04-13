using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.CatalogV2.Dto.BudgetaryStatusOfPayer
{
    public class BudgetaryStatusOfPayerDto
    {
        public long Id { get; set; }
        public BudgetaryPayerStatus Code { get; set; }
        public string Description { get; set; }
    }
}