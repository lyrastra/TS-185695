using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.TaxPostings.Enums;
using System;

namespace Moedelo.Money.Providing.Business.TaxPostings.Models
{
    public class OsnoTaxPosting : ITaxPosting, IDocumentId
    {
        public long DocumentId { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public TaxPostingDirection Direction { get; set; }
        public OsnoTransferType Type { get; set; }
        public OsnoTransferKind Kind { get; set; }
        public NormalizedCostType NormalizedCostType { get; set; }
    }
}
