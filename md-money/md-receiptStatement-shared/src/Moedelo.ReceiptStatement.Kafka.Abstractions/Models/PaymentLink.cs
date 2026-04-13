namespace Moedelo.ReceiptStatement.Kafka.Abstractions.Models
{
    public class PaymentLink
    {
        public long PaymentBaseId { get; set; }

        public decimal LinkSum { get; set; }
    }
}
