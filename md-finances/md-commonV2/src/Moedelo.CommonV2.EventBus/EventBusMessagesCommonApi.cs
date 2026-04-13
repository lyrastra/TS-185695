using Moedelo.CommonV2.EventBus.CommonApi;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<CurrentStepUserEvent> AnalyticsCurrentStepUserEvent;

        public static readonly EventBusEventDefinition<PrimaryNeedUserEvent> AnalyticsPrimaryNeedUserEvent;

        public static readonly EventBusEventDefinition<UserEnterInnEvent> CommonApiAnalyticsUserEnterInn;

        public static readonly EventBusEventDefinition<MasterOfRegistrationStepUpdatedEvent> MasterOfRegistrationStepUpdated;
    }
}