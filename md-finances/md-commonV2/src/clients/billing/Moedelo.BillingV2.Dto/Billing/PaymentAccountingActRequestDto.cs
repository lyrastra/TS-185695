using System;

namespace Moedelo.BillingV2.Dto.Billing
{
    public class PaymentAccountingActRequestDto
    {
        public int Id { get; set; }
        public int? OutsourcingTariff { get; set; }
        public int PriceListId { get; set; }
        public int FirmId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? IncomingDate { get; set; }
        public DateTime? DocumentDate { get; set; }
        public decimal Summ { get; set; }
        public string Positions { get; set; }
    }
}