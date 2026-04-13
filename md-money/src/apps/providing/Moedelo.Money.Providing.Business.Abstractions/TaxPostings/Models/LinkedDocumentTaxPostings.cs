using Moedelo.Money.Providing.Business.Abstractions.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models
{
    public class LinkedDocumentTaxPostings<T> : ILinkedDocumentTaxPostings<T>
        where T : ITaxPosting
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public LinkedDocumentType Type { get; set; }
        public IReadOnlyCollection<T> Postings { get; set; }

    }
}
