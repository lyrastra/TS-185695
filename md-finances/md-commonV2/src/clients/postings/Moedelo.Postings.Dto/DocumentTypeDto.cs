using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Postings.Dto
{
    public class DocumentTypeDto
    {
        public long Id { get; set; }

        public AccountingDocumentType DocumentType { get; set; }
    }
}