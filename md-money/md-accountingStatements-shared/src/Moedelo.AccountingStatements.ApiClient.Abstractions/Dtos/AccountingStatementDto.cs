using System;
using System.Collections.Generic;
using Moedelo.AccountingStatements.Enums;

namespace Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos
{
    public class AccountingStatementDto
    {
        public long? AccountingStatementId { get; set; }

        public DateTime Date { get; set; }

        public long Id { get; set; }

        public long? DocumentBaseId { get; set; }

        public string Number { get; set; }

        public AccountingStatementType Type { get; set; }

        public PrimaryDocumentsMoneyDirection Direction { get; set; }

        public string Description { get; set; }

        public IList<PostingDto> Postings { get; set; }

        public List<TaxPostingDto> TaxPostings { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}