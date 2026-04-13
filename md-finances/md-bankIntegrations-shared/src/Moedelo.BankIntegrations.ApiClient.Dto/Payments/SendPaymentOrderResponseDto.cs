using Moedelo.BankIntegrations.Dto.Payments;
using Moedelo.BankIntegrations.Enums;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Payments
{
    public class SendPaymentOrderResponseDto
    {
        public IntegrationResponseStatusCode StatusCode { get; set; }

        /// <summary> Ошибка отправки платежного поручения </summary>
        public SendPaymentErrorCode? ErrorCode { get; set; }

        /// <summary> Заштрихованная часть телефонного номера для OTP </summary>
        public string PhoneMask { get; set; }

        public string Message { get; set; }

        /// <summary> Если внешняя система назначает п/п свой идентификатор </summary>
        public List<ExternalDocumentDto> ExternalDocumentIds { get; set; } = new List<ExternalDocumentDto>();
    }
}
