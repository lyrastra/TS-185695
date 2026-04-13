using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Money.BankBalanceHistory.DataAccess.Extensions
{
    internal static class SettingRepositoryExtensions
    {
        internal static SettingValue GetBankBalanceHistoryConnectionString(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("BankBalanceHistoryConnectionString");
        }
    }
}
