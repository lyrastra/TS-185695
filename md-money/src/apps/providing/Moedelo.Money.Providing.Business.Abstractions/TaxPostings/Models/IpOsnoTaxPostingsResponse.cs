using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models
{
    public class IpOsnoTaxPostingsResponse : TaxPostingsResponse<IpOsnoTaxPosting>
    {
        public IpOsnoTaxPostingsResponse(TaxPostingStatus status)
            : base(status)
        {
        }
    }
}