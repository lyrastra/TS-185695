using System;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.TaxPostings;

namespace Moedelo.Postings.Dto
{
    public class LinkedDocumentDto
    {
        public long Id { get; set; }

        public DateTime DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public AccountingDocumentType DocumentType { get; set; }

        public decimal Sum { get; set; }

        public int CreateUserId { get; set; }

        public int ModifyUserId { get; set; }

        public TaxPostingStatus? TaxStatus { get; set; }
    }
}