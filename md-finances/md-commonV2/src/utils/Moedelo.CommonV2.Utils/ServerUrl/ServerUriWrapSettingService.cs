using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonV2.Utils.ServerUrl
{
    [InjectAsSingleton]
    public class ServerUriWrapSettingService : IServerUriWrapSettingService
    {
        private readonly SettingValue productionDomainPattern;
        private readonly SettingValue developmentHostPattern;
        private readonly SettingValue domainPattern;
        private readonly SettingValue productionSubdomainPatternWithMachineNumber;
        
        public ServerUriWrapSettingService(ISettingRepository settingRepository)
        {
            productionDomainPattern = settingRepository.Get("ServerUriProductionDomainPattern");
            developmentHostPattern = settingRepository.Get("ServerUriDevelopmentHostPattern");
            domainPattern = settingRepository.Get("ServerUriDomainPattern");
            productionSubdomainPatternWithMachineNumber = settingRepository.Get("ServerUriProductionSubdomainPatternWithMachineNumber");
        }

        public string GetProductionDomainPattern()
        {
            return productionDomainPattern.Value;
        }

        public string GetDevelopmentHostPattern()
        {
            return developmentHostPattern.Value;
        }

        public string GetDomainPattern()
        {
            return domainPattern.Value;
        }

        public string GetProductionSubdomainPatternWithMachineNumber()
        {
            return productionSubdomainPatternWithMachineNumber.Value;
        }
    }
}