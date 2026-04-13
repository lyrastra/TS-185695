using Moedelo.Common.Enums.Enums.Access;
using Moedelo.Common.Enums.Enums.WhiteLabels;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.CommonV2.WhiteLabel.Services
{
    [InjectAsSingleton(typeof(IWhiteLabelGetter))]
    internal sealed class WhiteLabelGetter : IWhiteLabelGetter
    {
        private readonly IUserInFirmAccessRulesReader userInFirmAccessRulesReader;

        public WhiteLabelGetter(
           IUserInFirmAccessRulesReader userInFirmAccessRulesReader)
        {
            this.userInFirmAccessRulesReader = userInFirmAccessRulesReader;

        }

        public bool IsWhiteLabel(IReadOnlyCollection<AccessRule> rules)
        {
            return rules.Contains(AccessRule.WlTariff);
        }

        public async Task<WhiteLabelType> GetAsync(int firmId, int userId)
        {
            var rules = await userInFirmAccessRulesReader.GetAsync(firmId, userId).ConfigureAwait(false);

            return GetByRules(rules.ToArray());
        }

        public WhiteLabelType GetByRules(IEnumerable<AccessRule> rules)
        {
            if (rules is ISet<AccessRule> rulesSet)
            {
                foreach (var rule in WhiteLabelMapping.PermissionWithWhiteLabelAccess)
                {
                    if (rulesSet.Contains(rule) &&
                        WhiteLabelMapping.PermissionToWhiteLabelType.TryGetValue(rule, out var type))
                    {
                        return type;
                    }
                }
            }
            else
            {
                foreach (var rule in rules)
                {
                    if (WhiteLabelMapping.PermissionToWhiteLabelType.TryGetValue(rule, out var type))
                    {
                        return type;
                    }
                }
            }

            return WhiteLabelType.Default;
        }
    }
}