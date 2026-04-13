using Moedelo.Accounts.Kafka.Abstractions.Events.Firm;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Firm
{
    /// <summary>
    /// Событие удаление аккаунта из реквизитов
    /// Физического удаления не происходит, только проставляется флаг is_deleted у фирмы и у пользователя
    /// </summary>
    public sealed class FirmMarkedIsDeletedEvent: FirmMarkedIsDeleted, IEntityEventData
    {
        
    }
}