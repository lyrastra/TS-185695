using System;
using Moedelo.Common.Enums.Enums.TaxPostings;

namespace Moedelo.AccountingV2.Dto.AccountingStatement
{
    public class TaxPostingDto
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string Description { get; set; } = string.Empty;

        public virtual TaxPostingsDirection Direction { get; set; }

        public virtual OsnoTransferType Type { get; set; }

        public virtual OsnoTransferKind Kind { get; set; }

        public virtual NormalizedCostType NormalizedCostType { get; set; }
    }
}
