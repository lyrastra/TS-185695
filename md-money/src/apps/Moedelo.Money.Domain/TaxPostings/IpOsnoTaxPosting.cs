using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Domain.TaxPostings
{
    public class IpOsnoTaxPosting
    {
        public TaxPostingDirection Direction { get; set; }

        public decimal Sum { get; set; }
    }
}
