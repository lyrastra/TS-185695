using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Stock.Kafka.Abstractions.RequisitionWaybills.Events
{
    public class RequisitionWaybillDeletedMessage : IEntityEventData
    {
        public long DocumentBaseId { get; set; }
    }
}