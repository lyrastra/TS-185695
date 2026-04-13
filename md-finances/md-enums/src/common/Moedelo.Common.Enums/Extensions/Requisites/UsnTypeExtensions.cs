using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.Common.Enums.Extensions.Requisites
{
    public static class UsnTypeExtensions
    {
        /// <summary> Получить название схемы УСН </summary>
        public static string GetTitle(this UsnTypes usnType)
        {
            switch (usnType)
            {
                case UsnTypes.Profit:
                    return "Доходы";
                case UsnTypes.ProfitAndOutgo:
                    return "Доходы-расходы";
                default:
                    return "Неизвестная схема УСН";
            }
        }

        public static bool IsProfit(this UsnTypes usnType)
        {
            return usnType == UsnTypes.Profit || usnType == UsnTypes.Default;
        }
    }
}