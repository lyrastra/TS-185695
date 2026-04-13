using System;
using System.Collections.Generic;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Commands.Patent
{
    public class OverwritePatentTaxPostingsCommand
    {
        public long DocumentBaseId { get; set; }

        // по дате документа можно проверить активность патента
        public DateTime DocumentDate { get; set; }

        public IReadOnlyCollection<PatentTaxPosting> Postings { get; set; }

        public TaxPostingStatus TaxStatus { get; set; }
    }
}
