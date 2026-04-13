using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class KontragentDocumentsSumsAndDirectionDto
    {
        public int KontragentId { get; set; }
        public PrimaryDocumentsTransferDirection Direction { get; set; }
        public DocumentsSumsDto Sums { get; set; }
    }
}
