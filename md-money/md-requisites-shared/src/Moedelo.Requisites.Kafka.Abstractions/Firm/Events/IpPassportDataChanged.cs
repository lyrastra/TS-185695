using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.Firm.Events
{
    public class IpPassportDataChanged : IEntityEventData
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime? Birthday { get; set; }
    }
}