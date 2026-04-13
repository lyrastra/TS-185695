using System;

namespace Moedelo.Money.Reports.Business.BankAndServiceBalanceReport.Models
{
    internal class LastFirmPayment
    {
        public int FirmId { get; set; }

        public string Tariff { get; set; }

        public string Product { get; set; }

        public int PaymentId { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
