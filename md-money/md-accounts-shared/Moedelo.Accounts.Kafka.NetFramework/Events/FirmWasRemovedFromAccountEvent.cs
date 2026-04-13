using Moedelo.Accounts.Kafka.Abstractions.Events;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Accounts.Kafka.NetFramework.Events
{
    /// <summary>
    /// Реализация события "Из аккаунта была удалена фирма" для .net framework
    /// </summary>
    public sealed class FirmWasRemovedFromAccountEvent : FirmWasRemovedFromAccountEventFields, IEntityEventData
    {
    }
}