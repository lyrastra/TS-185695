using System;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy.Dtos
{
    public class LinkedDocumentDto
    {
        public long Id { get; set; }

        public DateTime DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public LinkedDocumentType DocumentType { get; set; }

        public decimal Sum { get; set; }

        public int CreateUserId { get; set; }

        public int ModifyUserId { get; set; }

        public TaxPostingStatus? TaxStatus { get; set; }
    }
}