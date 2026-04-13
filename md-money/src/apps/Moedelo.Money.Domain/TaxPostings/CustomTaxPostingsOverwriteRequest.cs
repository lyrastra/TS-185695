using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.TaxPostings
{
    public class CustomTaxPostingsOverwriteRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public string Description { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public TaxPostingsData Postings { get; set; }
    }
}
