namespace Moedelo.Payroll.Kafka.Abstractions.Events
{
    public class PaymentEventNdfl
    {
        public int TaxDepartment { get; set; }
        public decimal TaxSum { get; set; }
    }
}