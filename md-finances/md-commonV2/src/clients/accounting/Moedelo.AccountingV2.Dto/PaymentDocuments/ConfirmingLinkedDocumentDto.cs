using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.PaymentDocuments
{
    public class ConfirmingLinkedDocumentDto
    {
        public AccountingDocumentType PrimaryDocumentType { get; set; }

        public int PrimaryDocumentId { get; set; }

        public decimal ConfirmingSum { get; set; }

        public int FirmId { get; set; }

        public long FinancialOperationBaseId { get; set; }
    }
}
