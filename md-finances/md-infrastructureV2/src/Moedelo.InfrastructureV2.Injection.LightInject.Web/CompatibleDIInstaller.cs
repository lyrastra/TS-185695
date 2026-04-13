using System.Reflection;
using LightInject;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.InfrastructureV2.Injection.LightInject.Web;

/// <summary>
/// Контейнер, метод GetInstance<TR> которого ищет stateless экземпляры сервисов, а не stateful
/// </summary>
public class CompatibleDIInstaller : WebDiInstaller
{
    public CompatibleDIInstaller(ILogger logger) : base(logger, Assembly.GetCallingAssembly())
    {
    }

    public override TR GetInstance<TR>()
    {
        return statelessContainer.GetInstance<TR>();
    }
}
