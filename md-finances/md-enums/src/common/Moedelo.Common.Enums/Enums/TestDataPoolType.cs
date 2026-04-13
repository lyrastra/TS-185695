using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums
{
    public enum TestDataPoolType
    {
        [Description("Не определен")]
        Default = 0,

        [Description("ИП БИЗ")]
        BizIp = 1,

        [Description("ООО БИЗ")]
        BizOoo = 2,

        [Description("ООО УСН Учетка")]
        UsnAccountant = 3,

        [Description("ООО ОСНО Учетка")]
        UsnOsno = 4
    }
}