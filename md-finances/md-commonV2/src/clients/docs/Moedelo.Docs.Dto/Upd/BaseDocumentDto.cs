using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Docs.Dto.Upd
{
    public class BaseDocumentDto
    {
        public long Id { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Number { get; set; }
        
        public AccountingDocumentType DocumentType { get; set; }
        
        public decimal Sum { get; set; }
    }
}