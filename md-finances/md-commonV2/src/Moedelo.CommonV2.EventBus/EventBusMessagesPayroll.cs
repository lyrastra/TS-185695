using Moedelo.CommonV2.EventBus.Payroll;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<WorkerUpdatedEvent> PayrollWorkerUpdated;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SzvTdUserEvent> SzvTdUserEvent;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<WorkerContractUserEvent> WorkerContractEvent;

        public static readonly EventBusEventDefinition<EfsChildCareUserEvent> EfsChildCareUserEvent;
    }
}