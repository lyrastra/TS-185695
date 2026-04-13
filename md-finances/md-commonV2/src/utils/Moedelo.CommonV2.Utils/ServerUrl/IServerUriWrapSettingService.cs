using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CommonV2.Utils.ServerUrl
{
    public interface IServerUriWrapSettingService : IDI
    {
        string GetProductionDomainPattern();

        string GetDevelopmentHostPattern();

        string GetDomainPattern();

        string GetProductionSubdomainPatternWithMachineNumber();
    }
}