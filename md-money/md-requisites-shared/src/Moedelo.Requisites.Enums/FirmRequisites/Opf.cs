using System.ComponentModel;

namespace Moedelo.Requisites.Enums.FirmRequisites
{
    public enum Opf
    {
        [Description("ИП")]
        IP = 1,
        [Description("ООО")]
        OOO = 2,
        [Description("ЗАО")]
        ZAO = 3,
        [Description("ОАО")]
        OAO = 4,
        [Description("НКО")]
        NKO = 5,
        [Description("АО")]
        AO = 6,
        [Description("ПАО")]
        PAO = 7
    }
}
