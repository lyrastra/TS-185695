using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Moedelo.AccPostings.Enums
{
    public static class SyntheticAccountCodeExtensions
    {
        private static readonly SyntheticAccountCode[] OffBalanceCodes = 
        {
            SyntheticAccountCode._001,
            SyntheticAccountCode._002,
            SyntheticAccountCode._004,
            SyntheticAccountCode._007,
            SyntheticAccountCode._012,
            SyntheticAccountCode._013,
            SyntheticAccountCode._017
        };

        public static SyntheticAccountBalanceType GetBalanceType(this SyntheticAccountCode code)
        {
            if (code == SyntheticAccountCode._000)
            {
                return SyntheticAccountBalanceType.Empty;
            }

            if (OffBalanceCodes.Contains(code))
            {
                return SyntheticAccountBalanceType.OffBalance;
            }

            return SyntheticAccountBalanceType.InBalance;
        }
        
        public static bool ContainsSubaccount(this SyntheticAccountCode code, SyntheticAccountCode subaccountCode)
        {
            if (code.GetBalanceType() != SyntheticAccountBalanceType.InBalance)
            {
                return code == subaccountCode;
            }

            var codeLevel = GetLevel(code);
            var subaccountCodeLevel = GetLevel(subaccountCode);

            if (subaccountCodeLevel > codeLevel)
            {
                return (long) code / 10000 % 100 == (long) subaccountCode / 10000 % 100;
            }
            
            return code == subaccountCode;
        }
        
        /// <summary>
        /// Получить порядок счета (актуально только для балансовых счетов)
        /// </summary>
        public static SyntheticAccountLevel GetLevel(this SyntheticAccountCode code)
        {
            var value = (int)code;
            if (value % 10000 == 0)
            {
                return SyntheticAccountLevel.First;
            }
            if (value % 100 == 0)
            {
                return SyntheticAccountLevel.Second;
            }
            
            return SyntheticAccountLevel.Third;
        }

        public static string GetAccountDisplayName(this SyntheticAccountCode code)
        {
            return code.GetDescription() ?? BuildAccountDisplayName(code);
        }

        public static string GetDescription(this Enum value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description;
        }

        private static string BuildAccountDisplayName(SyntheticAccountCode code)
        {
            var value = (int)code;

            var level3 = value % 100;
            var level2 = (value / 100) % 100;
            var level1 = (value / 10000) % 100;

            if (level3 != 0)
            {
                return $"{level1:00}.{level2:00}.{level3:00}";
            }
            if (level2 != 0)
            {
                return $"{level1:00}.{level2:00}";
            }
            return $"{level1:00}";
        }
    }
}