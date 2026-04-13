using System.ComponentModel;

namespace Moedelo.Money.Enums
{
    /// <summary>
    /// Ставки НДС
    /// </summary>
    /// <remarks>При добавлении новых ставок не забудь расширить метод GetNdsRatio</remarks>
    public enum NdsType
    {
        [Description("Без НДС")]
        None = -1,
        [Description("0%")]
        Nds0 = 0,
        [Description("10%")]
        Nds10 = 1,
        [Description("18%")]
        Nds18 = 2,
        [Description("10/110")]
        Nds10To110 = 3,
        [Description("18/118")]
        Nds18To118 = 4,
        [Description("20%")]
        Nds20 = 5,
        [Description("20/120")]
        Nds20To120 = 6,
        [Description("5%")]
        Nds5 = 7,
        [Description("5/105")]
        Nds5To105 = 8,
        [Description("7%")]
        Nds7 = 9,
        [Description("7/107")]
        Nds7To107 = 10,
        [Description("22%")]
        Nds22 = 11,
        [Description("22/122")]
        Nds22To122 = 12
    }
}
