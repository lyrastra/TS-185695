using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.PaymentDocuments
{
    public class ConfirmingDocumentsDto
    {
        public List<ConfirmingOperationDto> ConfirmingFinancialOperations { get; set; } = new List<ConfirmingOperationDto>();

        public long PrimaryDocumentBaseId { get; set; }

        public int FirmId { get; set; }
    }
}
