using Moedelo.Accounts.Kafka.Abstractions.Events.FirmAccess;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Accounts.Kafka.NetFramework.Events
{
    /// <summary>
    /// Реализация события "Получение доступа к фирме" для .net framework
    /// </summary>
    public sealed class FirmAccessGrantedEvent : FirmAccessGranted, IEntityEventData
    {
    }

    /// <summary>
    /// Реализация события "Изменение доступа к фирме" для .net framework
    /// </summary>
    public sealed class FirmAccessChangedEvent : FirmAccessChanged, IEntityEventData
    {
    }

    /// <summary>
    /// Реализация события "Доступ к фирме отозван" для .net framework
    /// </summary>
    public sealed class FirmAccessRevokedEvent : FirmAccessRevoked, IEntityEventData
    {
    }
}