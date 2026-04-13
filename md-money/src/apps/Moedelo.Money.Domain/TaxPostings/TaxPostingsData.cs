using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.TaxPostings
{
    public class TaxPostingsData
    {
        public ProvidePostingType ProvidePostingType { get; set; }

        public IReadOnlyCollection<UsnTaxPosting> UsnTaxPostings { get; set; } = Array.Empty<UsnTaxPosting>();

        public IpOsnoTaxPosting IpOsnoTaxPosting { get; set; }

        public IReadOnlyCollection<PatentTaxPosting> PatentTaxPostings { get; set; } = Array.Empty<PatentTaxPosting>();

        public IReadOnlyCollection<OsnoTaxPosting> OsnoTaxPostings { get; set; } = Array.Empty<OsnoTaxPosting>();
    }
}
