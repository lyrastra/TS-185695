using System.ComponentModel;

namespace Moedelo.Accounts.ApiClient.Enums.UserActions;

/// <summary>
/// Тип подтверждения
/// </summary>
public enum ConfirmationType : byte
{
    [Description("Подтверждение любым образом")]
    Any = 0,
    [Description("Подтверждение через email")]
    Email = 1,
    [Description("Подтверждение через sms")]
    Sms = 2,
    [Description("Подтверждение через звонок по телефону")]
    PhoneCall = 4
}
