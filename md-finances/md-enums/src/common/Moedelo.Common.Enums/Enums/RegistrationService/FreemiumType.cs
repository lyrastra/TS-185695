using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.RegistrationService
{
    public enum FreemiumType
    {
        [Description("Фримиум сроком на 6 мес.")]
        SixMonth = 1,
        [Description("Фримиум сроком на 12 мес.")]
        OneYear = 2,
        [Description("Фримиум навсегда")]
        Forever = 3,
        [Description("Фримиум сроком на 3 мес.")]
        ThreeMonth = 4
    }
}