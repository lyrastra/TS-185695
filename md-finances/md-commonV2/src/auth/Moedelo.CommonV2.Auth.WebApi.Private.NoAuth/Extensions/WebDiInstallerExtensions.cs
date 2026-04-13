using System;
using System.Reflection;
using System.Web.Http;
using Moedelo.InfrastructureV2.AuditWebApi.Extensions;
using Moedelo.InfrastructureV2.Injection.LightInject.Web;
using Moedelo.InfrastructureV2.Injection.LightInject.WebApi;
using Moedelo.InfrastructureV2.WebApi.Extensions;

namespace Moedelo.CommonV2.Auth.WebApi.Private.NoAuth.Extensions;

public static class WebDiInstallerExtensions
{
    public static void SetupMoedeloPrivateApiWebApplicationWithoutAuth(this WebDiInstaller diInstaller,
        Action<HttpConfiguration>? configurationCallback,
        params Assembly[] controllersAssembliesList)
    {
        diInstaller.Initialize();
        diInstaller.SetupWebApi(
            GlobalConfiguration.Configuration,
            controllersAssembliesList);

        GlobalConfiguration.Configure(
            httpConfiguration =>
            {
                httpConfiguration
                    .SetupMoedeloErrorLogging()
                    .SetupMoedeloAuditTrail(false);

                configurationCallback?.Invoke(httpConfiguration);
            });
    }
}
