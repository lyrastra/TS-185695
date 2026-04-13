using System;

namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class SendPaymentRequestResponseDto
    {
        /// <summary> Признак успеха отправки </summary>
        public bool IsSuccess { get; set; }

        /// <summary> Текст результата (при ошибке) </summary>
        public string Result { get; set; }

        /// <summary> Если внешняя система назначает п/п свой идентификатор </summary>
        public Guid ExternalDocumentId { get; set; }
    }
}