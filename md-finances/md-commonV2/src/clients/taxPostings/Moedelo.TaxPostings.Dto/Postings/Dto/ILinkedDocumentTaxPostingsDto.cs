using Moedelo.Common.Enums.Enums.Documents;
using System;
using System.Collections.Generic;

namespace Moedelo.TaxPostings.Dto.Postings.Dto
{
    public interface ILinkedDocumentTaxPostingsDto<out T> where T : ITaxPostingDto
    {
        long DocumentBaseId { get; }
        DateTime Date { get; }
        string Number { get; }
        AccountingDocumentType Type { get; }
        IReadOnlyCollection<T> Postings { get; }
    }
}
