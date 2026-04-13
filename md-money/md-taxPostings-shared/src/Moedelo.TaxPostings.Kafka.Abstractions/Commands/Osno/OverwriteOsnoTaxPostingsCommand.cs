using Moedelo.TaxPostings.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Commands.Osno
{
    public class OverwriteOsnoTaxPostingsCommand
    {
        public long DocumentBaseId { get; set; }

        public DateTime DocumentDate { get; set; }

        public IReadOnlyCollection<OsnoTaxPosting> Postings { get; set; }

        public TaxPostingStatus TaxStatus { get; set; }
    }
}
