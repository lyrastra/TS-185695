using Moedelo.Money.Providing.Business.TaxPostings.Models;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models
{
    public class UsnTaxPostingsResponse : TaxPostingsResponse<UsnTaxPosting>
    {
        public UsnTaxPostingsResponse(TaxPostingStatus status)
            : base(status)
        {
        }
    }
}
