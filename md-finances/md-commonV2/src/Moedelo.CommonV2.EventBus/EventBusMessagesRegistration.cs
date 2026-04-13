using Moedelo.CommonV2.EventBus.Registration;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<UserRegisteredEvent> RegistrationUserRegistered;

        public static readonly EventBusEventDefinition<RegionUpdateEvent> RegistrationBlRegionUpdate;

        public static readonly EventBusEventDefinition<ExperimentInfoEvent> RegistrationBlExperimentInfo;

        public static readonly EventBusEventDefinition<SberbankUsn6IpRegisteredWithAnotherEmail> SberbankUsn6IpRegisteredWithAnotherEmail;

        public static readonly EventBusEventDefinition<TestDataCreateEvent> RegistrationTestDataCreate;

        public static readonly EventBusEventDefinition<SendRegistrationMailCommand> RegistrationSendMail;

        public static readonly EventBusEventDefinition<AddTrialPaymentCommand> RegistrationAddTrialPayment;
    }
}