using System;

namespace Moedelo.Finances.Public.ClientData.Integrations
{
    public class SendPaymentOrderResponseClientData
    {
        public long DocumentBaseId { get; set; }
        public bool IsSuccess { get; set; }
        public string ExternalId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public decimal Sum { get; set; }
    }
}
