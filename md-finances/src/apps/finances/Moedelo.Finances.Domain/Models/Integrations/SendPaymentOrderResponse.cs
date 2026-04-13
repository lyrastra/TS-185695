using System;

namespace Moedelo.Finances.Domain.Models.Integrations
{
    public class SendPaymentOrderResponse
    {
        public long DocumentBaseId { get; set; }
        public string ExternalId { get; set; }
        public string Error { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string Number { get; set; }
    }
}
