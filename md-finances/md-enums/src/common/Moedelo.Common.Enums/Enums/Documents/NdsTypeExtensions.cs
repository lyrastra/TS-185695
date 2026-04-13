using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Enums.Nds;

namespace Moedelo.Common.Enums.Enums.Documents
{
    public static class NdsTypeExtensions
    {
        private static readonly Dictionary<NdsTypes, NdsType> NdsTypes2NdsType = new Dictionary<NdsTypes, NdsType>
        {
            [NdsTypes.None] = NdsType.WithoutNds,
            [NdsTypes.Nds0] = NdsType.Nds0,
            [NdsTypes.Nds5] = NdsType.Nds5,
            [NdsTypes.Nds7] = NdsType.Nds7,
            [NdsTypes.Nds10] = NdsType.Nds10,
            [NdsTypes.Nds18] = NdsType.Nds18,
            [NdsTypes.Nds20] = NdsType.Nds20,
            [NdsTypes.Nds22] = NdsType.Nds22
        };

        private static readonly Dictionary<NdsType, NdsTypes> NdsType2NdsTypes =
            NdsTypes2NdsType.ToDictionary(pair => pair.Value, pair => pair.Key);

        /// <summary>
        /// Получить ставку НДС в целых числах
        /// </summary>
        /// <returns>Для НДС 18% вернет 18, для 10% вернет 10 и т.п.</returns>
        public static decimal GetNdsPercent(this NdsType ndsType)
        {
            switch (ndsType)
            {
                case NdsType.Nds22: return 22;
                case NdsType.Nds20: return 20;
                case NdsType.Nds18: return 18;
                case NdsType.Nds10: return 10;
                case NdsType.Nds7: return 7;
                case NdsType.Nds5: return 5;
                case NdsType.UnknownNds:
                case NdsType.WithoutNds:
                case NdsType.Nds0:
                default:
                    return 0;
            }
        }

        public static NdsType GetNdsType(this NdsTypes value)
        {
            if (NdsTypes2NdsType.TryGetValue(value, out var type))
            {
                return type;
            }

            throw new ArgumentOutOfRangeException(nameof(value), value, $"Value {value} is not supported yet");
        }

        public static NdsTypes GetNdsTypes(this NdsType value)
        {
            if (NdsType2NdsTypes.TryGetValue(value, out var types))
            {
                return types;
            }

            if (value == NdsType.UnknownNds)
            { // the ugly special case
                return NdsTypes.None;
            }

            throw new ArgumentOutOfRangeException(nameof(value), value, $"Value {value} is not supported yet");
        }
    }
}