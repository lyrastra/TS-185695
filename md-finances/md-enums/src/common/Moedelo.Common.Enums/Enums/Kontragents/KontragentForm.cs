using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Kontragents
{
    public enum KontragentForm
    {
        [Description("Юрлицо")]
        UL = 1,
        [Description("ИП")]
        IP = 2,
        [Description("Физлицо")]
        FL = 3,
        [Description("Нерезидент")]
        NR = 4
    }
}