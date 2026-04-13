using Moedelo.Money.Providing.Business.Abstractions.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models
{
    public interface ILinkedDocumentTaxPostings<out T> where T : ITaxPosting
    {
        long DocumentBaseId { get; }
        DateTime Date { get; }
        string Number { get; }
        LinkedDocumentType Type { get; }
        IReadOnlyCollection<T> Postings { get; }
    }
}
