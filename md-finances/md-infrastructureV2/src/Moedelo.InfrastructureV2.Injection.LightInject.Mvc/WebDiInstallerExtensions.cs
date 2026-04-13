using System.Reflection;
using LightInject;
using Moedelo.InfrastructureV2.Injection.LightInject.Web;

namespace Moedelo.InfrastructureV2.Injection.LightInject.Mvc;

public static class WebDiInstallerExtensions
{
    /// <summary>
    /// Настроить di-инсталлер для работы c mvc в web-приложении
    /// ВИНМАНИЕ: метод должен вызываться из метода HttpApplication.Application_Start
    /// </summary>
    /// <param name="installer"></param>
    /// <param name="controllersAssembliesList"></param>
    /// <returns></returns>
    public static WebDiInstaller SetupMvc(
        this WebDiInstaller installer,
        params Assembly[] controllersAssembliesList)
    {
        installer.StatefulContainer.EnableMvc();
        installer.StatefulContainer.RegisterControllers(controllersAssembliesList);

        return installer;
    }
}