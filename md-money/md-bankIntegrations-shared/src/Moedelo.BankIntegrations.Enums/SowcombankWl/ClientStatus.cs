using System.Runtime.Serialization;

namespace Moedelo.BankIntegrations.Enums.SowcombankWl;

public enum ClientStatus
{
    [EnumMember(Value = "true")]
    Completed,

    [EnumMember(Value = "false")]
    Incomplete,

    [EnumMember(Value = "error")]
    Error
}
