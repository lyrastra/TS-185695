using System;
using System.Linq;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Extensions;

namespace Moedelo.Common.Enums.Enums.SyntheticAccounts
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

        public static string GetAccountDisplayName(this SyntheticAccountCode code)
        {
            return code.GetDescription() ?? BuildAccountDisplayName(code);
        }

        public static string[] GetNameSynonims(this SyntheticAccountCode code)
        {
            var memberInfo = typeof(SyntheticAccountCode).GetMember(code.ToString()).FirstOrDefault();

            if (memberInfo != null)
            {
                var description = (SyntheticAccountCodeDescriptionAttribute)memberInfo.GetCustomAttributes(typeof(SyntheticAccountCodeDescriptionAttribute), false)
                    .FirstOrDefault();

                if (description != null)
                {
                    return description.Synonims;
                }
            }

            return new string[] {};
        }

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
        
        [Obsolete("Use ContainsSubaccount instead (Wrong naming)")]
        public static bool IsCodeInAccount(this SyntheticAccountCode targetCode, SyntheticAccountCode checkedCode)
        {
            if (checkedCode.GetBalanceType() != SyntheticAccountBalanceType.InBalance)
            {
                return targetCode == checkedCode;
            }
            
            return (long) targetCode / 10000 % 100 == (long) checkedCode / 10000 % 100;
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
    }
}