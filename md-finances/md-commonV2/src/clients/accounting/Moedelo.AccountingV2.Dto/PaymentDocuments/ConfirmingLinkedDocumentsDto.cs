using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.PaymentDocuments
{
    public class ConfirmingLinkedDocumentsDto
    {
        public int FirmId { get; set; }

        public long FinancialOperationBaseId { get; set; }

        public List<ConfirmingLinkedDocumentDto> ConfirmingLinkedDocuments { get; set; } = new List<ConfirmingLinkedDocumentDto>();
    }
}
