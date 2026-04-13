namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargePayments
{
    public class ChargePaymentDto
    {
        public long ChargeId { get; set; }

        public int? ChargePaymentId { get; set; }

        public string Description { get; set; }

        public decimal Sum { get; set; }
    }
}
