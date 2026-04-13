using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Money.Numeration.DataAccess.PaymentOrders
{
    internal static class SettingRepositoryExtensions
    {
        internal static SettingValue GetPaymentOrderNumerationConnectionString(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("PaymentOrderNumerationConnectionString");
        }
    }
}
