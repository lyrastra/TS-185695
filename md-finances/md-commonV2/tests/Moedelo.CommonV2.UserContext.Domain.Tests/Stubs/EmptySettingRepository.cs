using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonV2.UserContext.Domain.Tests.Stubs;

[InjectAsSingleton(typeof(ISettingRepository))]
internal sealed class EmptySettingRepository : ISettingRepository
{
    public SettingValue Get(string settingName)
    {
        return new SettingValue(settingName, static _ => null);
    }
}
