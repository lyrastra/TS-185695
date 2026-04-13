using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonV2.WhiteLabel.Services
{
    [InjectAsSingleton(typeof(ISettingWrapService))]
    public class SettingWrapService: ISettingWrapService
    {
        private readonly SettingValue wlHostPattern;
        private readonly SettingValue sberWlHostPattern;
        private readonly SettingValue environment;

        public SettingWrapService(ISettingRepository settingRepository)
        {
            wlHostPattern = settingRepository.Get("WlHostPattern");
            sberWlHostPattern = settingRepository.Get("SberWlHostPattern");
            environment = settingRepository.Get("Environment");
        }

        public string GetWlHostPattern()
        {
            return wlHostPattern.Value;
        }

        public string GetSberWlHostPattern()
        {
            return sberWlHostPattern.Value;
        }

        public string GetEnvironment()
        {
            return environment.Value;
        }
    }
}