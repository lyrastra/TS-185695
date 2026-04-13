using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.TaxPostings.Enums;
using Moedelo.TaxPostings.Kafka.Abstractions.Usn.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Usn.Commands
{
    public class OverwriteUsnTaxPostings : IEntityCommandData
    {
        public long DocumentBaseId { get; set; }

        public DateTime DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public IReadOnlyCollection<UsnTaxPosting> Postings { get; set; }

        public TaxPostingStatus TaxStatus { get; set; }

        public long ProvidingStateId { get; set; }
    }
}
