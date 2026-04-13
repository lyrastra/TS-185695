using Moedelo.Catalog.ApiClient.Enums;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto
{
    public class BudgetaryStatusOfPayerDto
    {
        public long Id { get; set; }
        public BudgetaryPayerStatus Code { get; set; }
        public string Description { get; set; }
    }
}
