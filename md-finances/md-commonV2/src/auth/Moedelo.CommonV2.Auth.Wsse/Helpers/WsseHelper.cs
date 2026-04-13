using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Moedelo.Common.Enums.Enums.ExternalPartner;
using Moedelo.CommonV2.Auth.Wsse.Domain.Models;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;

namespace Moedelo.CommonV2.Auth.Wsse.Helpers
{
    public static class WsseHelper
    {
        public static WsseCheckResult Check(string wsseHeader, List<ExternalPartnerCredential> credentials, List<ExternalPartnerRule> rules, Func<string, string> hashFunc)
        {
            if (string.IsNullOrWhiteSpace(wsseHeader) || credentials == null || !credentials.Any())
            {
                return new WsseCheckResult { Error = "Missing credentials" };
            }

            var userNameMatch = Regex.Match(wsseHeader, @"UserName=""(?<UserName>\w*)""", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var passwordDigestMatch = Regex.Match(wsseHeader, @"PasswordDigest=""(?<PasswordDigest>.*?)""", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var nonceMatch = Regex.Match(wsseHeader, @"Nonce=""(?<Nonce>.*?)""", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var createdMatch = Regex.Match(wsseHeader, @"Created=""(?<Created>[A-Za-z0-9_\-:.=+]*)""", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            if (!userNameMatch.Success ||
                !passwordDigestMatch.Success ||
                !nonceMatch.Success ||
                !createdMatch.Success)
            {
                return new WsseCheckResult { Error = "Missing credentials" };
            }

            var userName = userNameMatch.Groups["UserName"].Value;
            var passwordDigest = passwordDigestMatch.Groups["PasswordDigest"].Value;
            var base64Nonce = nonceMatch.Groups["Nonce"].Value;
            var createdString = createdMatch.Groups["Created"].Value;

            if (!DateTime.TryParse(createdString, out var parseDate))
            {
                return new WsseCheckResult { InvalidDataError = $"Created : {createdString}" };
            }

            var createdUniversalTime = parseDate.ToUniversalTime();

            var credential = credentials.FirstOrDefault(x => x.UserName == userName);

            if (credential == null)
            {
                return new WsseCheckResult { Error = "Invalid credentials" };
            }

            var utcNow = DateTime.UtcNow;

            if (utcNow.Date > credential.ExpiryDate?.Date)
            {
                return new WsseCheckResult {Error = "Your credentials have expired"};
            }
            
            var hasAccess = rules == null || rules.All(credential.Rules.Contains);

            if (hasAccess &&
                createdUniversalTime > utcNow.AddMinutes(-2) &&
                createdUniversalTime <= utcNow.AddMinutes(2))
            {
                byte[] nonceData = null;
                try
                {
                    nonceData = Convert.FromBase64String(base64Nonce);
                }
                catch (FormatException)
                {
                    return new WsseCheckResult { InvalidDataError = $"Nonce : {base64Nonce}" };
                }

                string nonce = Encoding.UTF8.GetString(nonceData);

                var result = hashFunc($"{nonce}{createdString}{credential.Secret}");

                if (result == passwordDigest)
                {
                    return new WsseCheckResult { Partner = credential };
                }
            }

            return new WsseCheckResult { Error = "Invalid credentials" };
        }

        public static List<ExternalPartnerRule> GetPartnerRules(this IHttpEnviroment enviroment)
        {
            object value;

            if (!enviroment.ItemList.TryGetValue("PartnerRules", out value))
            {
                return null;
            }

            return value as List<ExternalPartnerRule>;
        }

        public static int? GetParnerId(this IHttpEnviroment enviroment)
        {
            object value;

            if (!enviroment.ItemList.TryGetValue("PartnerId", out value))
            {
                return null;
            }

            return value as int?;
        }
    }
}
