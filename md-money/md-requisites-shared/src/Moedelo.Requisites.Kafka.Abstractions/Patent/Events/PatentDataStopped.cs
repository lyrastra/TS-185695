using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.Patent.Events
{
    public class PatentDataStopped : IEntityEventData
    {
        public long Id { get; set; }

        /// <summary>
        /// Id фирмы
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Код патента
        /// </summary>
        public string Code { get; set; }
    }
}
