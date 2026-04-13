using Moedelo.BankIntegrations.IntegrationPartnersInfo.Extensions;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.Finances.Business.Helpers.Money
{
    public static class PurseIntegrationHelper
    {
        public static IntegrationPartners? GetPartnerByPurseName(string purseName)
        {
            if (string.IsNullOrEmpty(purseName))
            {
                return null;
            }

            if (purseName == IntegrationPartners.YandexKassa.GetInfo(IntegrationPartnersExtentions.PartnerName))
            {
                return IntegrationPartners.YandexKassa;
            }

            if (purseName == IntegrationPartners.YMoney.GetInfo(IntegrationPartnersExtentions.PartnerName))
            {
                return IntegrationPartners.YMoney;
            }

            return null;
        }
    }
}
