using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.PaymentOrder
{
    public class PaymentOrderForServiceResponseDto
    {
        public int FirmId { get; set; }

        public bool IsSuccess { get; set; }

        public AccountingDocumentType CreatedDocumentType { get; set; }

        public long PaymentId { get; set; }

        public long? DocumentBaseId { get; set; }
    }
}