#nullable enable
using System;
using System.Reflection;
using System.Web.Http;
using Moedelo.InfrastructureV2.AuditWebApi.Extensions;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Injection.LightInject.Web;
using Moedelo.InfrastructureV2.Injection.LightInject.WebApi;
using Moedelo.InfrastructureV2.WebApi.Extensions;

namespace Moedelo.CommonV2.Auth.WebApi.Extensions;

internal static class WebDiInstallerExtensions
{
    internal static void ConfigureMoedeloWebApiApplication(this WebDiInstaller diInstaller,
        Assembly[] controllersAssembliesList,
        Action<HttpConfiguration>? configurationCallback,
        bool saveClientIpAddressInAuditTrail, bool saveUserAgent,
        Action<IDiRegistry> addServiceRegistration)
    {
        diInstaller.Initialize(addServiceRegistration);
        diInstaller.SetupWebApi(
            GlobalConfiguration.Configuration,
            controllersAssembliesList);

        GlobalConfiguration.Configure(
            httpConfiguration =>
            {
                httpConfiguration
                    .SetupMoedeloExceptionLogger()
                    .SetupMoedeloAuditTrail(saveClientIpAddressInAuditTrail,saveUserAgent)
                    .SetupMoedeloAuthentication();

                configurationCallback?.Invoke(httpConfiguration);
            });
    }
}
