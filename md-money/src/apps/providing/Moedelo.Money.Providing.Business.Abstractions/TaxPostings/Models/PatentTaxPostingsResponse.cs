using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models
{
    public class PatentTaxPostingsResponse : TaxPostingsResponse<PatentTaxPosting>
    {
        public PatentTaxPostingsResponse(TaxPostingStatus status)
            : base(status)
        {
        }
    }
}
