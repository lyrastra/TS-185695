using Moedelo.Accounts.Kafka.Abstractions.Events;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Accounts.Kafka.NetFramework.Events
{
    /// <summary>
    /// Реализация события "Объединение аккаунтов: другой аккаунт был присоединён к этому аккаунту" для .net framework
    /// </summary>
    public sealed class AccountWasMergedWithAnotherAccountEvent : AccountWasMergedWithAnotherAccountEventFields, IEntityEventData
    {
    }
}