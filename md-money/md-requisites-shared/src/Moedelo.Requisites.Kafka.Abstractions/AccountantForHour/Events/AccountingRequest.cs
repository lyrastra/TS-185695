using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.AccountantForHour.Events
{
    public class AccountingRequest : IEntityEventData
    {
        public int FirmId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }
    }
}