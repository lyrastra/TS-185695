using System.Reflection;
using System.Web;
using LightInject;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Injection.Lightinject;
using Moedelo.InfrastructureV2.Injection.LightInject.Web.Internals;

namespace Moedelo.InfrastructureV2.Injection.LightInject.Web;

public partial class WebDiInstaller : AppAutoDiInstaller, IDIChecks
{
    public ServiceContainer StatefulContainer => statefulContainer;

    public WebDiInstaller(ILogger logger, Assembly callingAssembly = null)
        : base(logger, callingAssembly ?? Assembly.GetCallingAssembly())
    {
    }

    protected override string AssemblyPath
    {
        get
        {
            try
            {
                //упадет если не в IIS т.е. режим тестирования/без iis
                return $"{HttpRuntime.AppDomainAppPath}bin";
            }
            catch
            {
                //режим без iis
                return base.AssemblyPath;
            }
        }
    }

    protected override void RegisterBehaviour()
    {
        RegisterSingleton<IDIChecks>(s => this);

        base.RegisterBehaviour();
    }

    protected override IScopeManagerProvider CreateScopeManagerProviderForStatefulContainer()
    {
        return new MoedeloPerWebRequestScopeManagerProvider();
    }
}
