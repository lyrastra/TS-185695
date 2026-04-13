using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Moedelo.Common.Utils.ServerUrl
{
    public static class DomainService
    {
        public static string GetBaseDomain(string url)
        {
            var (level3, level2, level1, machineNumber) = GetDisassembledBaseDomain(url);

            if (string.IsNullOrEmpty(level3))
            {
                level3 = "www";
            }

            return $"{level3}{machineNumber}.{level2}.{level1}";
        }

        public static string GetOauthDomain(string url)
        {
            var (level3, level2, level1, machineNumber) = GetDisassembledBaseDomain(url);

            level3 = string.IsNullOrEmpty(level3) ? "oauth" : $"oauth-{level3}";

            return $"{level3}{machineNumber}.{level2}.{level1}";
        }

        public static string GetSsoDomain(string url)
        {
            var (level3, level2, level1, machineNumber) = GetDisassembledBaseDomain(url);

            level3 = string.IsNullOrEmpty(level3) ? "sso" : $"sso-{level3}";

            return $"{level3}{machineNumber}.{level2}.{level1}";
        }

        private static (string level3, string level2, string level1, string machineNumber) GetDisassembledBaseDomain(string url)
        {
            var parts = url.Split('.');
            var length = parts.Length;

            var level1 = parts.ElementAtOrDefault(length - 1);
            var level2 = parts.ElementAtOrDefault(length - 2);
            var level3 = parts.ElementAtOrDefault(length - 3);

            if (string.IsNullOrEmpty(level1) || string.IsNullOrEmpty(level2))
            {
                throw new Exception();
            }

            if (string.IsNullOrEmpty(level3))
            {
                return (string.Empty, level2, level1, string.Empty);
            }

            string digits = string.Empty;

            var digitMatch = Regex.Match(level3, @"oauth(\d*)|sso(\d*)|www(\d*)");
            if (digitMatch.Success)
            {
                digits = digitMatch.Groups[1].Value + digitMatch.Groups[2].Value + digitMatch.Groups[3].Value;
            }

            level3 = Regex.Replace(level3, @"oauth\d*-?|sso\d*-?|www\d*|public\d*-?|private\d*-?|restapi\d*-?|eds\d*-?", string.Empty);

            return (level3, level2, level1, digits);
        }
    }
}
