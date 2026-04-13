using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.Firm.Events
{
    public class DirectorChanged : IEntityEventData
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }
        public DateTime? Birthday { get; set; }
    }
}