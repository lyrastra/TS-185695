using Moedelo.Accounts.Kafka.Abstractions.Events.Firm;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Accounts.Kafka.NetFramework.Events.Firm
{
    public sealed class FirmIsInternalChangedEvent : FirmIsInternalChanged, IEntityEventData
    {
    }
}