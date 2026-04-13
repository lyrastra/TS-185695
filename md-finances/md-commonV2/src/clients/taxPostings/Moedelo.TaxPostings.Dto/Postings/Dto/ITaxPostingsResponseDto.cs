using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.TaxPostings;
using System.Collections.Generic;

namespace Moedelo.TaxPostings.Dto.Postings.Dto
{
    public interface ITaxPostingsResponseDto<out T> where T : ITaxPostingDto
    {
        IReadOnlyCollection<ILinkedDocumentTaxPostingsDto<T>> LinkedDocuments { get; }

        IReadOnlyCollection<T> Postings { get; }

        TaxPostingStatus TaxStatus { get; }

        string ExplainingMessage { get; }

        TaxationSystemType TaxationSystemType { get; }
    }
}
