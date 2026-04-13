using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Konragents.Enums;

namespace Moedelo.Konragents.Kafka.Abstractions.Kontragent.Events
{
    public class KontragentArchived : IEntityEventData
    {
        public int Id { get; set; }
        public string Inn { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Fio { get; set; }
        public KontragentForm? Form { get; set; }
        public bool IsCommissionAgent { get; set; }
    }
}
