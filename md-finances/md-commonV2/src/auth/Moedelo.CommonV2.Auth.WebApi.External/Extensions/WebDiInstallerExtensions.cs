using System;
using System.Net;
using System.Reflection;
using System.Web.Http;
using Moedelo.CommonV2.Auth.Domain;
using Moedelo.CommonV2.Auth.External;
using Moedelo.CommonV2.Auth.WebApi.Extensions;
using Moedelo.CommonV2.Auth.WebApi.External.Enums;
using Moedelo.CommonV2.Xss.WebApi.Extensions;
using Moedelo.InfrastructureV2.Injection.LightInject.Web;
using Moedelo.InfrastructureV2.WebApi.Extensions;
using Moedelo.InfrastructureV2.WebApi.Validation.Extensions;
using Moedelo.InfrastructureV2.WebApi.Validation.Handlers;

namespace Moedelo.CommonV2.Auth.WebApi.External.Extensions;

public static class WebDiInstallerExtensions
{
    public static void SetupMoedeloExternalApiWebApplication(this WebDiInstaller diInstaller,
        Action<HttpConfiguration>? configurationCallback,
        params Assembly[] controllersAssembliesList)
    {
        diInstaller.SetupMoedeloExternalApiWebApplication(configurationCallback,
            XssValidationMode.RejectSuspiciousRequests,
            controllersAssembliesList);
    }

    public static void SetupMoedeloExternalApiWebApplication(this WebDiInstaller diInstaller,
        Action<HttpConfiguration>? configurationCallback,
        XssValidationMode xssValidationMode,
        params Assembly[] controllersAssembliesList)
    {
        diInstaller.SetupMoedeloExternalApiWebApplication(
            settings =>
            {
                settings.XssValidationMode = xssValidationMode;
            },
            configurationCallback,
            controllersAssembliesList);
    }

    public static void SetupMoedeloExternalApiWebApplication(this WebDiInstaller diInstaller,
        Action<MoedeloExternalApiWebApplicationConfigurationSettings> setupSettingsCallback,
        Action<HttpConfiguration>? configurationCallback,
        params Assembly[] controllersAssembliesList)
    {
        var settings = new MoedeloExternalApiWebApplicationConfigurationSettings(); 
        setupSettingsCallback.Invoke(settings);

        diInstaller.ConfigureMoedeloWebApiApplication(
            controllersAssembliesList,
            configurationCallback: configuration =>
            {
                configuration.SetupMoedeloXssProtection(settings.XssValidationMode);
                configuration.IncludeErrorDetailPolicy = settings.IncludeErrorDetailPolicy;

                if (settings.RejectEmptyPostAndPutRequests)
                {
                    configuration.SetupEmptyPostAndPutRequestRejection();
                }

                if (settings.ValidationMode.HasFlag(ValidationMode.Enable))
                {
                    configuration.SetupValidationFilter(settings.ValidationMode.HasFlag(ValidationMode.AddDebugInfoIntoResponse));

                    if (settings.ValidationMode.HasFlag(ValidationMode.SuppressValidationErrorLogging))
                    {
                        configuration.SetupExceptionLogger<WebApiNoValidationUnhandledExceptionLogger>(replace: true);
                    }
                    
                    configuration.SetupExceptionHandler<UnhandledExceptionHandler>();
                }

                configurationCallback?.Invoke(configuration);
            },
            saveClientIpAddressInAuditTrail: true,
            saveUserAgent: true,
            diRegistry =>
                diRegistry.RegisterSingleton<IAuthenticationService, ExternalAuthenticationService>());

        // включаем поддержку TLS 1.2 для публичного API
        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
    }
}
