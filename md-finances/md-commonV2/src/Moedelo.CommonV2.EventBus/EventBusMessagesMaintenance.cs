using Moedelo.CommonV2.EventBus.Maintenance;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus;

public partial class EventBusMessages
{
    /// <summary>
    /// Событие "Фирмы были удалены"
    /// </summary>
    // ReSharper disable once UnassignedReadonlyField
    public static readonly EventBusEventDefinition<FirmsDeletedEvent> FirmsDeleted;
}