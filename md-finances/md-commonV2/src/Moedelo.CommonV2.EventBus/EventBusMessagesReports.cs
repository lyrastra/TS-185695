using Moedelo.CommonV2.EventBus.Reports;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<ChangeWizardStatusEvent> ReportsChangedWizardStatus;

        public static readonly EventBusEventDefinition<WizardClosedEvent> ReportsWizardClosed;

        public static readonly EventBusEventDefinition<SendKudirByEmailCommand> SendKudirByEmail;
    }
}