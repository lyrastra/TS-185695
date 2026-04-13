using System.Collections.Generic;
using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.Dto.Payments
{
    public class SendPaymentOrdersResponseDto : BaseResponseDto
    {
        public SendPaymentErrorCode? ErrorCode { get; set; }
        public List<ExternalDocumentDto> ExernalDocuments { get; set; }
    }
}