namespace Moedelo.Money.Kafka.Abstractions.Models
{
    public class BillLink
    {
        public long BillBaseId { get; set; }

        public decimal LinkSum { get; set; }
    }
}
