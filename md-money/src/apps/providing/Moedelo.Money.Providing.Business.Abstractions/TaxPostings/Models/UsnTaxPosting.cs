using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.TaxPostings.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.TaxPostings.Models
{
    public class UsnTaxPosting : ITaxPosting, IDocumentId, IRelatedDocumentBaseIds
    {
        public long DocumentId { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public TaxPostingDirection Direction { get; set; }
        public string Description { get; set; }
        public IReadOnlyCollection<long> RelatedDocumentBaseIds { get; set; }
    }
}
