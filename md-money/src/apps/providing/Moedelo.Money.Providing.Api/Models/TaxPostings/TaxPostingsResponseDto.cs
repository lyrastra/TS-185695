using Moedelo.Requisites.Enums.TaxationSystems;
using Moedelo.TaxPostings.Enums;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Api.Models.TaxPostings
{
    public class TaxPostingsResponseDto<T> where T : ITaxPostingDto
    {
        public IReadOnlyCollection<LinkedDocumentTaxPostingsDto<T>> LinkedDocuments { get; set; }

        public IReadOnlyCollection<T> Postings { get; set; }

        public string ExplainingMessage { get; set; }
    }

    public class TaxPostingsResponseDto : TaxPostingsResponseDto<ITaxPostingDto>
    {
        public TaxationSystemType TaxationSystemType { get; set; }

        public TaxPostingStatus TaxStatus { get; set; }
    }
}
