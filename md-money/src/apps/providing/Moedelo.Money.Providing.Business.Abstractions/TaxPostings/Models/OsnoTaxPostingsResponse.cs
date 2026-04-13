using Moedelo.Money.Providing.Business.TaxPostings.Models;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models
{
    public class OsnoTaxPostingsResponse : TaxPostingsResponse<OsnoTaxPosting>
    {
        public OsnoTaxPostingsResponse(TaxPostingStatus status)
            : base(status)
        {
        }
    }
}
