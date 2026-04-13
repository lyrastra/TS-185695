using System;
using System.Collections.Generic;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models
{
    public class PatentTaxPosting : ITaxPosting, IDocumentId, IRelatedDocumentBaseIds
    {
        public long DocumentId { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string Description { get; set; }
        public TaxPostingDirection Direction { get; set; }
        public string DocumentNumber { get; set; }
        public IReadOnlyCollection<long> RelatedDocumentBaseIds { get; set; }
    }
}
