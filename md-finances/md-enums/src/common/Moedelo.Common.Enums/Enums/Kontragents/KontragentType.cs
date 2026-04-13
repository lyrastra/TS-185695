using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Kontragents
{
    public enum KontragentType
    {
        [Description("Требует внимания")]
        Undefined = 0,

        [Description("Покупатель/Поставщик")]
        Kontragent = 1,

        [Description("Покупатель")]
        Client = 2,

        [Description("Поставщик")]
        Partner = 3,

        [Description("Другое")]
        Other = 4
    }
}