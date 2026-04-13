using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Money.PurseOperations.DataAccess.Extensions
{
    internal static class SettingRepositoryExtensions
    {
        internal static SettingValue GetConnectionString(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("ConnectionString");
        }

        internal static SettingValue GetReadOnlyConnectionString(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("ReadOnlyConnectionString");
        }
    }
}