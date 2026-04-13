using Moedelo.TaxPostings.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Commands.Usn
{
    public class OverwriteUsnTaxPostingsCommand
    {
        public long DocumentBaseId { get; set; }

        public DateTime DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public IReadOnlyCollection<UsnTaxPosting> Postings { get; set; }

        public TaxPostingStatus TaxStatus { get; set; }
    }
}
