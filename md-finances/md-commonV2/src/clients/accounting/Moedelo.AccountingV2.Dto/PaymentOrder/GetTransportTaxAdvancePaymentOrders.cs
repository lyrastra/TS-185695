namespace Moedelo.AccountingV2.Dto.PaymentOrder
{
    public class GetTransportTaxAdvancePaymentOrders
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }

        public int Year { get; set; }
        public string Oktmo { get; set; }
    }
}
