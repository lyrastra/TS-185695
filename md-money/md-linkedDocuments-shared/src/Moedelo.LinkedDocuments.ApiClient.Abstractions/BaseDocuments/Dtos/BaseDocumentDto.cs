using System;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos
{
    public class BaseDocumentDto
    {
        public long Id { get; set; }
        
        public LinkedDocumentType Type { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public TaxPostingStatus? TaxStatus { get; set; }
        
        public int FirmId { get; set; }
    }
}
