namespace Moedelo.BillingV2.Dto.Billing
{
    public class PositionByPaymentDto
    {
        public decimal Summa { get; set; }

        public string ServiceName { get; set; }

        public bool HasNds { get; set; }
    }
}
