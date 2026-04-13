using System;
using Moedelo.Common.Enums.Enums.TaxPostings;

namespace Moedelo.Finances.Domain.Models.Money.Operations.TaxPostings
{
    public class TaxPosting
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public TaxPostingsDirection Direction { get; set; }

        public string Description { get; set; }

        public OsnoTransferType Type { get; set; }

        public OsnoTransferKind Kind { get; set; }

        public NormalizedCostType NormalizedCostType { get; set; }
    }
}
