namespace Moedelo.Money.Enums.Extensions
{
    public static class NdsTypeExtensions
    {
        /// <summary>
        /// Возвращает значение коэффициента для ставки НДС
        /// </summary>
        public static decimal GetNdsRatio(this NdsType type)
        {
            return type switch
            {
                NdsType.None or NdsType.Nds0 => 0m,
                NdsType.Nds10 or NdsType.Nds10To110 => 10m / 110m,
                NdsType.Nds18 or NdsType.Nds18To118 => 18m / 118m,
                NdsType.Nds20 or NdsType.Nds20To120 => 20m / 120m,
                NdsType.Nds5 or NdsType.Nds5To105 => 5m / 105m,
                NdsType.Nds7 or NdsType.Nds7To107 => 7m / 107m,
                NdsType.Nds22 or NdsType.Nds22To122 => 22m / 122m,
                _ => 0m
            };
        }
    }
}