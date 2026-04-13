using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Estate.Kafka.Abstractions.Events.EventData.InventoryCard
{
    public class InventoryCardDeletedMessage : IEntityEventData
    {
        /// <summary>
        /// Идентификатор инвентарной карточки
        /// </summary>
        public long DocumentBaseId { get; set; }
    }
}