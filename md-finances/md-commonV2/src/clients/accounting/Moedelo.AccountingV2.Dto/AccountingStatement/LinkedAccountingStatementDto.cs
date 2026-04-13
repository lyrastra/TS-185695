using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.AccountingStatement
{
    public class LinkedAccountingStatementDto
    {
        public DateTime Date { get; set; }

        public long SourceDocumentBaseId { get; set; }

        public List<PostingDto> Postings { get; set; }
    }
}