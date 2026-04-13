using Moedelo.Accounts.Kafka.Abstractions.Events.FirmAccess;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.FirmAccess
{
    /// <summary>
    /// Реализация события "Получение доступа к фирме" для .net core
    /// </summary>
    public sealed class FirmAccessGrantedEvent : FirmAccessGranted, IEntityEventData
    {
    }
}