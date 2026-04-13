using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.Utils.ServerUrl;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.CommonV2.WhiteLabel.Services
{
    [InjectAsSingleton(typeof(IWhiteLabelService))]
    public class WhiteLabelService : IWhiteLabelService
    {
        public string GetNameByHost(string host)
        {
            // sber
            if (host.Contains("sber"))
            {
                return "sber";
            }

            //sovcombank
            if (host.Contains("sovcombank"))
            {
                return "sovcombank";
            }

            // wbbank (Вайлдберриз Банк)
            // Прод-домен buh.wb-bank.ru: DomainService разбирает Level3="buh",
            // без явной проверки возвращается "buh" вместо "wbbank" →
            // WhiteLabelMapping не находит тип, WL CSS не грузится (403 на st.mdstatic.org).
            // На box/stage regex wl-(\w+) срабатывает корректно.
            if (host.Contains("wb-bank"))
            {
                return "wbbank";
            }

            // localhost
            if (host.Contains("localhost"))
            {
                var match = Regex.Match(host, @"wl-(\w+)\.localhost");
                return match.Groups.Count == 1
                    ? null
                    : match.Groups[1].Value;
            }

            // md
            var domainParts = DomainService.GetDisassembledBaseDomain(host);
            if (string.IsNullOrEmpty(domainParts.Level3) || Regex.IsMatch(domainParts.Level3, @"^(stage|box\d+)"))
            {
                return null;
            }

            // wl-...-stage/box
            var matchWlUrl = Regex.Match(domainParts.Level3, @"wl-(\w+)(-stage|-box\d+)");
            if (matchWlUrl.Groups.Count > 1)
            {
                return matchWlUrl.Groups[1].Value;
            }

            // wl prod
            return domainParts.Level3;
        }

        [Obsolete("Use WhiteLabelGetter.IsWhiteLabel instead")]
        public bool IsTariffWl(ISet<AccessRule> accessRules)
        {
            return accessRules.Contains(AccessRule.WlTariff);
        }
    }
}