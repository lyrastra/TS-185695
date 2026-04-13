using Moedelo.Accounts.Kafka.Abstractions.Events;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Accounts.Kafka.NetFramework.Events
{
    /// <summary>
    /// Реализация события "В аккаунт была добавлена новая фирма" для .net framework
    /// </summary>
    public sealed class NewFirmWasCreatedInAccountEvent : NewFirmWasCreatedInAccountEventFields, IEntityEventData
    {
    }
}