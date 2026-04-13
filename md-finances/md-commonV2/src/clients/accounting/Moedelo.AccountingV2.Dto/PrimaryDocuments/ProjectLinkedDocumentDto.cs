using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class ProjectLinkedDocumentDto
    {
        public long Id { get; set; }

        public long BaseId { get; set; }

        public string Date { get; set; }

        public string Name { get; set; }

        public decimal Sum { get; set; }

        public PrimaryDocumentsTransferDirection Direction { get; set; }

        public AccountingDocumentType DocumentType { get; set; }
    }
}