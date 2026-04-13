using Moedelo.TaxPostings.Enums;
using System;

namespace Moedelo.Money.Domain.TaxPostings
{
    public class UsnTaxPosting
    {
        public decimal Sum { get; set; }

        public TaxPostingDirection Direction { get; set; }

        public string Description { get; set; }

        public long DocumentBaseId { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string DocumentNumber { get; set; }
    }
}
