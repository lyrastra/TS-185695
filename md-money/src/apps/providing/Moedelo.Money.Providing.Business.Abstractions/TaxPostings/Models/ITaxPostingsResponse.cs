using Moedelo.Requisites.Enums.TaxationSystems;
using Moedelo.TaxPostings.Enums;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models
{
    public interface ITaxPostingsResponse<out T> where T : ITaxPosting
    {
        IReadOnlyCollection<ILinkedDocumentTaxPostings<T>> LinkedDocuments { get; }

        IReadOnlyCollection<T> Postings { get; }

        TaxPostingStatus TaxStatus { get; }

        string Message { get; }

        TaxationSystemType TaxationSystemType { get; }
    }
}
