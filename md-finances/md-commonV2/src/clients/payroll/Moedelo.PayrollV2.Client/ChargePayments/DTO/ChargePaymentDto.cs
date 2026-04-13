namespace Moedelo.PayrollV2.Client.ChargePayments.DTO
{
    public class ChargePaymentDto
    {
        public long ChargeId { get; set; }

        public int? ChargePaymentId { get; set; }

        public string Description { get; set; }

        public bool CanApplyDeduction { get; set; }

        public decimal Sum { get; set; }
    }
}
