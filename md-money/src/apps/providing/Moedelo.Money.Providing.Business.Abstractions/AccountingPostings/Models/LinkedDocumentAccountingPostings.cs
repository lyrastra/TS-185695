using Moedelo.Money.Providing.Business.Abstractions.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models
{
    public class LinkedDocumentAccountingPostings
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public LinkedDocumentType Type { get; set; }

        public IReadOnlyCollection<AccountingPosting> Postings { get; set; } = Array.Empty<AccountingPosting>();
    }
}
