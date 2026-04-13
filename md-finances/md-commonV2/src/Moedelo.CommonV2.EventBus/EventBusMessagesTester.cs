using Moedelo.CommonV2.EventBus.Tester;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<CreateUserEvent> CreateUserFromTester;
    }
}
