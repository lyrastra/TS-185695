using System;
using System.Web;
using LightInject;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;
using Moedelo.InfrastructureV2.Injection.LightInject.Web.Internals;

namespace Moedelo.InfrastructureV2.Injection.LightInject.Web;

public static class WebDiInstallerExtensions
{
    /// <summary>
    /// Установить поддержку perWebRequest scope для web-приложения
    /// ВНИМАНИЕ: должен вызываться из метода HttpApplication.Init 
    /// </summary>
    /// <param name="installer"></param>
    /// <param name="application"></param>
    public static void SetupPerWebRequestScope(this WebDiInstaller installer, HttpApplication application)
    {
        application.BeginRequest += (sender, args) =>
        {
            var context = ((HttpApplication)sender).Context; 
            context.Items[HttpContextItemNames.MdScopeKey] = installer.BeginScope();
            // форсируем создание экземпляра IHttpEnviroment пока не потерян HttpContext.Current 
            installer.GetInstance<IHttpEnviroment>();
        };

        application.EndRequest += static (sender, args) =>
        {
            var context = ((HttpApplication)sender).Context;

            if (context.Items.Contains(HttpContextItemNames.MdScopeKey))
            {
                (context.Items[HttpContextItemNames.MdScopeKey] as IDisposable)?.Dispose();
                context.Items.Remove(HttpContextItemNames.MdScopeKey);
            }
        };

        application.OnExecuteRequestStep((context, step) =>
        {
            // поскольку BeginRequest и EndRequest имеют собственный ExecutionContext,
            // надо восстановить значение текущего DI-скоупа
            var scopeManager = installer.StatefulContainer.ScopeManagerProvider.GetScopeManager(installer.StatefulContainer) as MoedeloPerWebRequestScopeManager;

            if (scopeManager?.CurrentScope == null && context.Items.Contains(HttpContextItemNames.MdScopeKey))
            {
                scopeManager?.RestoreRootScopeIfMissed(context.Items[HttpContextItemNames.MdScopeKey] as Scope);
            }

            step();
        });
    }

    public static WebDiInstaller SetupWebPagesActivator(this WebDiInstaller installer)
    {
        HttpRuntime.WebObjectActivator = new MoedeloWebObjectActivator(
            installer,
            HttpRuntime.WebObjectActivator);

        return installer;
    }
}