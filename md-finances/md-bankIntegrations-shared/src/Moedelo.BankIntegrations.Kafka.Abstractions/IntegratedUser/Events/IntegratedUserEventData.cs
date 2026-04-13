using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.BankIntegrations.Kafka.Abstractions.IntegratedUser.Events
{
    public class IntegratedUserEventData : IEntityEventData
    {
        public int FirmId { get; set; }
        /// <summary> С кем интеграция </summary>
        public IntegrationPartners IntegrationPartner { get; set; }
        public bool isActive { get; set; }
    }
}