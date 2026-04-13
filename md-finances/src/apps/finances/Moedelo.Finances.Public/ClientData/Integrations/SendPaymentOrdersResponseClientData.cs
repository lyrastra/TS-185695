using Moedelo.BankIntegrations.Enums;
using System.Collections.Generic;

namespace Moedelo.Finances.Public.ClientData.Integrations
{
    public class SendPaymentOrdersResponseClientData
    {
        public IntegrationResponseStatusCode StatusCode { get; set; }
        public SendPaymentErrorCode? ErrorCode { get; set; }
        public string PhoneMask { get; set; }
        public string Message { get; set; }
        public List<SendPaymentOrderResponseClientData> List { get; set; }
    }
}
