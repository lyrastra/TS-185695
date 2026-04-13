using System;
using System.Web;
using Moedelo.InfrastructureV2.Injection.LightInject.Web;
using Moedelo.InfrastructureV2.WebApi.Handlers;
using Moedelo.CommonV2.Auth.WebApi.External.Extensions;
using Moedelo.InfrastructureV2.Logging;
using Moedelo.InfrastructureV2.WebApi.Extensions;

namespace Moedelo.Finances.Public;

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
        Installer.SetupMoedeloExternalApiWebApplication(
            settings => settings.EnableValidation()
                .SetRejectEmptyPostAndPutRequests(true),
            config =>
            {
                config.SetupExceptionHandler<UnhandledExceptionHandler>();
                WebApiConfig.Register(config);
            },
            typeof(WebApiApplication).Assembly);
    }

    protected void Application_End()
    {
        Installer.Dispose();
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin",
            HttpContext.Current.Request.Headers["Origin"] ?? "");
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
        HttpContext.Current.Response.AddHeader("Access-Control-Expose-Headers", "Content-Disposition");

        if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers",
                "Content-Type, Accept, Cache-control, pragma");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
            HttpContext.Current.Response.End();
        }
    }
}
