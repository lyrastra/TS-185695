using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesWaybills.Models
{
    public class PurchasesWaybillItemDto
    {
        public NdsType NdsType { get; set; }
        public decimal NdsSum { get; set; }
        public decimal SumWithNds { get; set; }
        public decimal SumWithoutNds { get; set; }
    }
}