using System;

namespace Moedelo.BillingV2.Client.BillingApi
{
    public class BillingDto
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public int TariffId { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}