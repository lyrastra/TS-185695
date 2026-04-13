using Moedelo.BankIntegrationsV2.Dto.Integrations;
using Moedelo.SberbankCryptoEndpointV2.Dto.SberbankMoedeloToken;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.PaymentRequest
{
    /// <summary>
    /// Платежное требование
    /// </summary>
    public class SendPaymentRequestDto
    {
        /// <summary>
        /// Платежное требование фирме
        /// </summary>
        public PaymentOrderDto PaymentOrder { get; set; }
        /// <summary>
        /// Клиент МД который выставляет требование
        /// </summary>
        public string ClientId { get; set; }
    }
}