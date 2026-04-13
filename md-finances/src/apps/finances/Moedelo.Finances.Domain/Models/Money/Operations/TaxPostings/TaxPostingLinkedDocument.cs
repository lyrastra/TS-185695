using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Finances.Domain.Models.Money.Operations.TaxPostings
{
    public class TaxPostingLinkedDocument
    {
        public string DocumentName { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DocumentDate { get; set; }

        public AccountingDocumentType Type { get; set; }

        public List<TaxPosting> Postings { get; set; }
    }
}
