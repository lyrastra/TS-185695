using System;

namespace Moedelo.Docs.Enums.Extensions
{
    public static class NdsTypeExtensions
    {
        /// <summary>
        /// Получить ставку НДС в целых числах
        /// </summary>
        /// <returns>Для НДС 18% вернет 18, для 10% вернет 10 и т.п.</returns>
        public static decimal GetNdsPercent(this NdsType ndsType)
        {
            return ndsType switch
            {
                NdsType.Nds22 => 22,
                NdsType.Nds20 => 20,
                NdsType.Nds18 => 18,
                NdsType.Nds10 => 10,
                NdsType.Nds7 => 7,
                NdsType.Nds5 => 5,
                _ => 0
            };
        }
        
        public static NdsTypes ToNdsTypes(this NdsType sourceType)
        {
            return sourceType switch
            {
                NdsType.WithoutNds => NdsTypes.None,
                NdsType.Nds0 => NdsTypes.Nds0,
                NdsType.Nds5 => NdsTypes.Nds5,
                NdsType.Nds7 => NdsTypes.Nds7,
                NdsType.Nds10 => NdsTypes.Nds10,
                NdsType.Nds18 => NdsTypes.Nds18,
                NdsType.Nds20 => NdsTypes.Nds20,
                NdsType.Nds22 => NdsTypes.Nds22,
                _ => throw new ArgumentOutOfRangeException(nameof(sourceType), sourceType, $"Not supported ndsType: {sourceType}")
            };
        }
    }
}