namespace Moedelo.Stock.Kafka.Abstractions.RequisitionWaybills.Events
{
    public class RequisitionWaybillItemSaveMessage
    {
        public decimal Count { get; set; }

        public long ProductId { get; set; }
    }
}