using Moedelo.CommonV2.EventBus.Account;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus;

public partial class EventBusMessages
{
    // ReSharper disable UnassignedReadonlyField
    public static readonly EventBusEventDefinition<UserLoginCountUpdateEvent> AccountBlUserLoginCountUpdate;
    public static readonly EventBusEventDefinition<AccountBlPartnerFixationChangedEvent> AccountBlPartnerFixationChanged;
    public static readonly EventBusEventDefinition<AccountBlUserLoginChangedEvent> AccountBlUserLoginChanged;
    public static readonly EventBusEventDefinition<UserRegistrationNameChangedEvent> UserRegistrationNameChanged;
    public static readonly EventBusEventDefinition<FirmLinkedServiceAccountChangedEvent> FirmLinkedServiceAccountChanged;
    public static readonly EventBusEventDefinition<FirmAccountChangedEvent> FirmAccountChanged;
    public static readonly EventBusEventDefinition<AccountFirmIsInternalChangedEvent> AccountFirmIsInternalChanged;
    public static readonly EventBusEventDefinition<AccountFirmIsDeletedChangedEvent> AccountFirmIsDeletedChanged;
    // ReSharper restore UnassignedReadonlyField
}