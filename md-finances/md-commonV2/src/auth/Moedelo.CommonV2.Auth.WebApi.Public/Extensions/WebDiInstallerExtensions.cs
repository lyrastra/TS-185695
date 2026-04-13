using System;
using System.Net;
using System.Reflection;
using System.Web.Http;
using Moedelo.CommonV2.Auth.Domain;
using Moedelo.CommonV2.Auth.OAuth2;
using Moedelo.CommonV2.Auth.WebApi.Extensions;
using Moedelo.CommonV2.Xss.WebApi.Extensions;
using Moedelo.InfrastructureV2.Injection.LightInject.Web;

namespace Moedelo.CommonV2.Auth.WebApi.Public.Extensions;

public static class WebDiInstallerExtensions
{
    public static void SetupMoedeloPublicApiWebApplication(this WebDiInstaller diInstaller,
        Action<HttpConfiguration>? configurationCallback,
        params Assembly[] controllersAssembliesList)
    {
        diInstaller.SetupMoedeloPublicApiWebApplication(
            configurationCallback,
            XssValidationMode.RejectSuspiciousRequests,
            controllersAssembliesList);
    }
    
    public static void SetupMoedeloPublicApiWebApplication(this WebDiInstaller diInstaller,
        Action<HttpConfiguration>? configurationCallback,
        XssValidationMode xssValidationMode,
        params Assembly[] controllersAssembliesList)
    {
        diInstaller.ConfigureMoedeloWebApiApplication(
            controllersAssembliesList,
            configurationCallback: configuration =>
            {
                configuration.SetupMoedeloXssProtection(xssValidationMode);

                configurationCallback?.Invoke(configuration);
            },
            saveClientIpAddressInAuditTrail: true,
            saveUserAgent: true,
            diRegistry => diRegistry.RegisterSingleton<IAuthenticationService, OAuth2AuthenticationService>());

        // включаем поддержку TLS 1.2 для публичного API
        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
    }
}
