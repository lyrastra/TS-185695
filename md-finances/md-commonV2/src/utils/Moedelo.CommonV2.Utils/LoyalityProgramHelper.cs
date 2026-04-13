using System;
using System.Collections.Generic;

namespace Moedelo.CommonV2.Utils
{
    public static class LoyalityProgramHelper
    {
        private const string OneMonthBonus = "100859907";

        private const string TwoMonthBonus = "200493955";

        private const string ThreeMonthBonus = "300593445";

        private const string FourMonthBonus = "400411554";

        private static readonly List<string> BasePromoCodes;
        
        static LoyalityProgramHelper()
        {
            BasePromoCodes = new List<string>
                {
                    OneMonthBonus,
                    TwoMonthBonus,
                    ThreeMonthBonus,
                    FourMonthBonus
                };
        }

        public static int GetCountOfMonthToExpirationDate(DateTime expirationDate)
        {
            var nowDate = DateTime.Now;
            var correctingValue = expirationDate.Day >= nowDate.Day ? 0 : -1;
            return ((expirationDate.Year - nowDate.Year) * 12) + expirationDate.Month - nowDate.Month + correctingValue;
        }

        public static long GeneratePersonalPromoCode(int firmId, int countOfMonthForExpirationDate)
        {
            long promoCode = 0;
            long tmpVal = 0;
            var castResult = false;

            switch (countOfMonthForExpirationDate)
            {
                case 4:
                    castResult = long.TryParse(FourMonthBonus, out tmpVal);
                    break;
                case 3:
                    castResult = long.TryParse(ThreeMonthBonus, out tmpVal);
                    break;
                case 2:
                    castResult = long.TryParse(TwoMonthBonus, out tmpVal);
                    break;
                case 1:
                    castResult = long.TryParse(OneMonthBonus, out tmpVal);
                    break;
            }

            if (castResult)
            {
                promoCode = tmpVal ^ firmId;
            }

            return promoCode;
        }

        public static bool IsLoyalityProgramPromoCode(int firmId, string promoCode)
        {
            var basePromoCode = GetBasePromoCodeFromPersonal(firmId, promoCode);

            return !string.IsNullOrEmpty(basePromoCode) && BasePromoCodes.Contains(basePromoCode);
        }

        public static bool IsLoyalityProgram(int countOfMonth)
        {
            const int maxCountOfMonthBeforeExpiration = 4;

            return countOfMonth <= maxCountOfMonthBeforeExpiration && countOfMonth > 0;
        }

        public static bool IsValidPromoCode(string promoCode, DateTime expirationDate)
        {
            var countOfMonth = GetCountOfMonthToExpirationDate(expirationDate);

            switch (promoCode)
            {
                case FourMonthBonus when countOfMonth == 4:
                case ThreeMonthBonus when countOfMonth == 3:
                case TwoMonthBonus when countOfMonth == 2:
                case OneMonthBonus when countOfMonth == 1:
                    return true;
                default:
                    return false;
            }
        }

        public static string GetBasePromoCodeFromPersonal(int firmId, string promoCode)
        {
            var castResult = long.TryParse(promoCode, out var basePromoCode);

            if (!castResult)
            {
                return string.Empty;
            }
            
            basePromoCode ^= firmId;
            return basePromoCode.ToString();

        }
    }
}