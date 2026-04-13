using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.IntegrationPartnersInfo.Helpers
{
    public static class IntegrationPartnersBikHelper
    {
        // Нужен в случаях когда в IntegrationPartnerInfoAttribute.regexpNameInDb не совпадает с наименованием в таблице Bank или наименования у партнеров одинаковые
        private static readonly Dictionary<IntegrationPartners, List<string>> dictionary = new Dictionary<IntegrationPartners, List<string>>
        {
            {
                IntegrationPartners.PsBank, new List<string>{"040813744","044525555","041806715","044030920","045004816","046577975","040702773","042202803","047888760","043510135","046711119","044371122","042157106"}
            }
        };

        public static List<string> GetBikByIntegrationPartners(IntegrationPartners partner)
        {
            return dictionary.ContainsKey(partner) ? dictionary[partner] : new List<string>();
        }

        public static string GetStringBikByIntegrationPartners(IntegrationPartners partner)
        {
            return dictionary.ContainsKey(partner) ? string.Join(",", dictionary[partner]) : null;
        }
    }
}
