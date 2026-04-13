using Moedelo.CommonV2.EventBus.Requisities;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<NoTimeEvent> RequisitiesBlNoTime;
        
        public static readonly EventBusEventDefinition<FirmFssInfoChangedEvent> FirmFssInfoChanged;
        public static readonly EventBusEventDefinition<FirmCashModeChangedEvent> FirmCashModeChanged;

        public static readonly EventBusEventDefinition<RequisitiesFirmEvent> RequisitiesBlUpdated;

        public static readonly EventBusEventDefinition<RequisitiesFirmEvent> RequisitiesBlFirmDeleted;

        public static readonly EventBusEventDefinition<UpdateRegistrationDataEvent> RequisitesBlRegDataUpdated;

        public static readonly EventBusEventDefinition<RequisitesChangedEvent> RequisitesChanged;

        public static readonly EventBusEventDefinition<TaxationSystemChangedEvent> TaxationSystemChanged;

        public static readonly EventBusEventDefinition<TradingObjectChangedEvent> TradingObjectChanged;

        public static readonly EventBusEventDefinition<TradingObjectDeletedEvent> TradingObjectDeleted;

        public static readonly EventBusEventDefinition<SettlementAccountChangedEvent> SettlementAccountChanged;

        public static readonly EventBusEventDefinition<RptSettingsUpdatedEvent> RptSettingsUpdated;

        public static readonly EventBusEventDefinition<OnboardingRequisitesCompletedEvent> OnboardingRequisitesCompleted;

        public static readonly EventBusEventDefinition<PatentDataChangedEvent> PatentDataChanged;

        public static readonly EventBusEventDefinition<HourlyAccountantTaskCommand> HourlyAccountantTask;
    }
}
