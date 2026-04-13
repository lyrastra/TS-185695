using System.Web;
using Moedelo.CommonV2.Auth.WebApi.Private.Extensions;
using Moedelo.InfrastructureV2.Injection.LightInject.Web;
using Moedelo.InfrastructureV2.Logging;

namespace Moedelo.Finances.Api;

public class WebApiApplication : HttpApplication
{
    private static readonly WebDiInstaller Installer = new WebDiInstaller(new Logger());

    public override void Init()
    {
        base.Init();

        Installer.SetupPerWebRequestScope(this);
    }

    protected void Application_Start()
    {
        Installer.SetupMoedeloPrivateApiWebApplication(
            WebApiConfig.Register,
            typeof(WebApiApplication).Assembly);
    }

    protected void Application_End()
    {
        Installer.Dispose();
    }
}
