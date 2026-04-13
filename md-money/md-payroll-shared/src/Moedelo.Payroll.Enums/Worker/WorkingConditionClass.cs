using System.ComponentModel;

namespace Moedelo.Payroll.Enums.Worker;

public enum WorkingConditionClass : byte
{
    [Description("1")]
    Optimal = 1,
    [Description("2")]
    Acceptable,
    [Description("3.1")]
    Harmful1,
    [Description("3.2")]
    Harmful2,
    [Description("3.3")]
    Harmful3,
    [Description("3.4")]
    Harmful4,
    [Description("4")]
    Dangerous
}