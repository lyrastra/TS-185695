#nullable enable
using System;
using System.Net;
using System.Reflection;
using System.Web.Http;
using Moedelo.CommonV2.Auth.Domain;
using Moedelo.CommonV2.Auth.Private;
using Moedelo.CommonV2.Auth.WebApi.Extensions;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Injection.LightInject.Web;

namespace Moedelo.CommonV2.Auth.WebApi.Private.Extensions;

public static class WebDiInstallerExtensions
{
    public static void SetupMoedeloPrivateApiWebApplication(this WebDiInstaller diInstaller,
        Action<HttpConfiguration>? configurationCallback,
        params Assembly[] controllersAssembliesList)
    {
        diInstaller.SetupMoedeloPrivateApiWebApplication(
            _ => { }, configurationCallback, controllersAssembliesList);
    }

    public static void SetupMoedeloPrivateApiWebApplication(this WebDiInstaller diInstaller,
        Action<IDiRegistry> addServiceRegistration,
        Action<HttpConfiguration>? configurationCallback,
        params Assembly[] controllersAssembliesList)
    {
        diInstaller.ConfigureMoedeloWebApiApplication(
            controllersAssembliesList,
            configurationCallback: configurationCallback,
            saveClientIpAddressInAuditTrail: false,
            saveUserAgent: false,
            diRegistry =>
            {
                addServiceRegistration.Invoke(diRegistry);
                diRegistry.RegisterSingleton<IAuthenticationService, PrivateAuthenticationService>();
            });
        
        // включаем поддержку TLS 1.2 для приватного API
        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
    }
}
