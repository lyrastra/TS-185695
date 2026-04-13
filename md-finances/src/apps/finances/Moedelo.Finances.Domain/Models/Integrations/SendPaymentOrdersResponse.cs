using Moedelo.BankIntegrations.Enums;
using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Integrations
{
    public class SendPaymentOrdersResponse
    {
        public IntegrationResponseStatusCode StatusCode { get; set; }
        public SendPaymentErrorCode? ErrorCode { get; set; }
        public string PhoneMask { get; set; }
        public string Message { get; set; }
        public List<SendPaymentOrderResponse> List { get; set; } = new List<SendPaymentOrderResponse>();
    }
}
