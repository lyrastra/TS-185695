using Moedelo.Requisites.Enums.TaxationSystems;
using Moedelo.TaxPostings.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models
{
    public class TaxPostingsResponse<T> : ITaxPostingsResponse<T>
        where T : ITaxPosting
    {
        protected TaxPostingsResponse(TaxPostingStatus status)
        {
            TaxStatus = status;
        }

        public IReadOnlyCollection<ILinkedDocumentTaxPostings<T>> LinkedDocuments { get; set; } = Array.Empty<LinkedDocumentTaxPostings<T>>();

        public IReadOnlyCollection<T> Postings { get; set; } = Array.Empty<T>();

        public TaxPostingStatus TaxStatus { get; set; }

        public string Message { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }
    }

    public class TaxPostingsResponse : TaxPostingsResponse<ITaxPosting>
    {
        public TaxPostingsResponse()
            : base(TaxPostingStatus.NotTax)
        {
        }
    }
}
