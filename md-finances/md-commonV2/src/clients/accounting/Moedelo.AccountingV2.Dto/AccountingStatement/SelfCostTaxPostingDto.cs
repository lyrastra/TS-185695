using System;
using Moedelo.Common.Enums.Enums.TaxPostings;

namespace Moedelo.AccountingV2.Dto.AccountingStatement
{
    public class SelfCostTaxPostingDto
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public string DocumentNumber { get; set; }

        public TaxPostingsDirection Direction { get; set; }
    }
}
