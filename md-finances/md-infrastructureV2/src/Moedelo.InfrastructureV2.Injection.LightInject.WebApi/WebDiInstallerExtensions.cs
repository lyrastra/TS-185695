using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using LightInject;
using Moedelo.InfrastructureV2.Injection.LightInject.Web;
using Moedelo.InfrastructureV2.Injection.LightInject.WebApi.Internals;

namespace Moedelo.InfrastructureV2.Injection.LightInject.WebApi;

public static class WebDiInstallerExtensions
{
    /// <summary>
    /// Настроить di-инсталлер для работы с webapi в web-приложении
    /// ВИНМАНИЕ: метод должен вызываться из метода HttpApplication.Application_Start
    /// </summary>
    /// <param name="installer"></param>
    /// <param name="httpConfiguration"></param>
    /// <param name="controllersAssembliesList"></param>
    /// <returns></returns>
    public static WebDiInstaller SetupWebApi(
        this WebDiInstaller installer,
        HttpConfiguration httpConfiguration,
        params Assembly[] controllersAssembliesList)
    {
        installer.StatefulContainer.EnableWebApi(httpConfiguration);
        installer.StatefulContainer.RegisterApiControllers(controllersAssembliesList);

        var controllerActivator = httpConfiguration.Services.GetService(typeof(IHttpControllerActivator)) as IHttpControllerActivator
                                  ?? throw new ArgumentNullException(nameof(IHttpControllerActivator));
        httpConfiguration.Services.Replace(typeof(IHttpControllerActivator),new MdWebApiControllerActivator(controllerActivator));

        return installer;
    }
}