using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesStatements.Models
{
    public class PurchasesStatementsItemDto
    {
        public NdsType NdsType { get; set; }
        public decimal NdsSum { get; set; }
        public decimal SumWithNds { get; set; }
        public decimal SumWithoutNds { get; set; }
    }
}