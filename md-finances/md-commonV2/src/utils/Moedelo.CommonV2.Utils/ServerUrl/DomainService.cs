using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Moedelo.CommonV2.Utils.ServerUrl
{
    public static class DomainService
    {
        public static string GetBaseDomain(string url)
        {
            var domainParts = GetDisassembledBaseDomain(url);

            if (string.IsNullOrEmpty(domainParts.Level3))
            {
                domainParts.Level3 = "www";
            }

            return domainParts.BuildFullName();
        }

        public static string GetOauthDomain(string url)
        {
            var domainParts = GetDisassembledBaseDomain(url);

            if (string.IsNullOrEmpty(domainParts.Level3))
            {
                domainParts.Level3 = "oauth";
            }
            else
            {
                domainParts.Level3 = $"oauth-{domainParts.Level3}";
            }

            return domainParts.BuildFullName();
        }

        public static string GetSsoDomain(string url)
        {
            var domainParts = GetDisassembledBaseDomain(url);

            if (string.IsNullOrEmpty(domainParts.Level3))
            {
                domainParts.Level3 = "sso";
            }
            else
            {
                domainParts.Level3 = $"sso-{domainParts.Level3}";
            }

            return domainParts.BuildFullName();
        }

        public static DomainParts GetDisassembledBaseDomain(string url)
        {
            var parts = url.Split('.');
            var length = parts.Length;

            var level1 = parts.ElementAtOrDefault(length - 1);
            var level2 = parts.ElementAtOrDefault(length - 2);
            var level3 = parts.ElementAtOrDefault(length - 3);

            // reverting part order if org.moedelo...
            if (level3 == "org")
            {
                var temp = level3;
                level3 = level1;
                level1 = temp;
            }

            if (string.IsNullOrEmpty(level1) || string.IsNullOrEmpty(level2))
            {
                throw new Exception();
            }

            if (string.IsNullOrEmpty(level3))
            {
                return new DomainParts
                {
                    Level1 = level1,
                    Level2 = level2,
                    Level3 = level3,
                    MachineNumber = string.Empty
                };
            }

            string digits = string.Empty;

            var digitMatch = Regex.Match(level3, @"oauth(\d*)|sso(\d*)|www(\d*)");
            if (digitMatch.Success)
            {
                digits = digitMatch.Groups[1].Value + digitMatch.Groups[2].Value + digitMatch.Groups[3].Value;
            }

            level3 = Regex.Replace(level3, @"oauth\d*-?|sso\d*-?|www\d*|public\d*-?|private\d*-?|restapi\d*-?|eds\d*-?", string.Empty);

            return new DomainParts
            {
                Level1 = level1,
                Level2 = level2,
                Level3 = level3,
                MachineNumber = digits
            };
        }
    }
}