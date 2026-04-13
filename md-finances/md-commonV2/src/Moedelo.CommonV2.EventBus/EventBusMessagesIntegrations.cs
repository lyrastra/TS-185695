using Moedelo.CommonV2.EventBus.Integrations;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<MovementListEvent> IntegrationsBlMovementListEvent;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<RobokassaMovementListEvent> IntegrationsRobokassaMovementListEvent;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SettlementBalanceEvent> IntegrationsSettlementBalanceEventEvent;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<MovementListReviseEvent> IntegrationsMovementListReviseEvent;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<MovementListReviseForUserEvent> IntegrationsMovementListReviseForUserEvent;
        
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<IntegratedUserChangeEvent> IntegrationsChangeIntegratedUser;
    }
}