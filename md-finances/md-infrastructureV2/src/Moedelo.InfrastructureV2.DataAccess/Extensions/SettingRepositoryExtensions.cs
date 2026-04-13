using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.InfrastructureV2.DataAccess.Extensions;

public static class SettingRepositoryExtensions
{
    public static SettingValue GetMoedeloConnectionString(this ISettingRepository settingRepository)
        => settingRepository.GetRequired("ConnectionString");
    
    public static SettingValue GetMoedeloReadOnlyConnectionString(this ISettingRepository settingRepository)
        => settingRepository.GetRequired("ReadOnlyConnectionString");
}
