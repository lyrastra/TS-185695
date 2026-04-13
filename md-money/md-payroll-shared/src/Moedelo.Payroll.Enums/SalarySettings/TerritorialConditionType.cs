using System.ComponentModel;

namespace Moedelo.Payroll.Enums.SalarySettings
{
    public enum TerritorialConditionType
    {
        [Description("Нет")]
        None = 0,

        [Description("РКС — Район Крайнего Севера")]
        RKS = 1,

        [Description("МКС — Местность, приравненная к району Крайнего Севера")]
        MKS = 2,

        [Description("РКСР — Район крайнего Севера")]
        RKSR = 3,

        [Description("МКСР — местность, приравненная к району Крайнего Севера")]
        MKSR = 4,

        [Description("МКС-РКСР — МКС на 31.12.2001 г. и РКС на текущую дату")]
        MKSRKSR = 5,

        [Description("ОКУ — Местность с особыми климатическими условиями, за исключением РКС и МКС")]
        OKU = 6,
    }
}