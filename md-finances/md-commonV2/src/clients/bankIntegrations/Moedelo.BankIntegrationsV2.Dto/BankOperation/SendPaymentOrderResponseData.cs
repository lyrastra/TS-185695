using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BankIntegrationsV2.Dto.BankOperation
{
    public class SendPaymentOrderResponseData
    {
        public IntegrationResponseStatusCode StatusCode { get; set; }

        /// <summary> Заштрихованная часть телефонного номера для OTP </summary>
        public string PhoneMask { get; set; }

        public string Message { get; set; }

        /// <summary> Если внешняя система назначает п/п свой идентификатор </summary>
        public List<ExternalDocumentDto> ExternalDocumentIds { get; set; } = new List<ExternalDocumentDto>();
    }
}