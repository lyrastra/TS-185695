using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Moedelo.CommonV2.Auth.WebApi.Extensions;
using Moedelo.CommonV2.Webpack;
using Moedelo.CommonV2.Webpack.Services;
using Moedelo.CommonV2.Xss.WebApi.Extensions;
using Moedelo.InfrastructureV2.AuditMvc;
using Moedelo.InfrastructureV2.AuditWebApi.Extensions;
using Moedelo.InfrastructureV2.Injection.LightInject.Mvc;
using Moedelo.InfrastructureV2.Injection.LightInject.Web;
using Moedelo.InfrastructureV2.Injection.LightInject.WebApi;
using Moedelo.InfrastructureV2.Logging;
using Moedelo.InfrastructureV2.WebApi.Extensions;
using UnhandledExceptionHandler = Moedelo.Finances.WebApp.Infrastructure.WebApi.UnhandledExceptionHandler;

namespace Moedelo.Finances.WebApp
{
    public class Global : HttpApplication
    {
        private static readonly WebDiInstaller Installer = new WebDiInstaller(new Logger());

        public override void Init()
        {
            base.Init();

            Installer.SetupPerWebRequestScope(this);
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();

            Installer.Initialize();
            Installer.SetupWebApi(GlobalConfiguration.Configuration, typeof(Global).Assembly);
            Installer.SetupMvc(typeof(Global).Assembly);

            WebpackInitializer.Init(Installer.GetInstance<IWebpackService>());

            GlobalConfiguration.Configure(config =>
                {
                    config.SetupMoedeloAuthentication()
                        .SetupMoedeloExceptionLogger()
                        .SetupExceptionHandler<UnhandledExceptionHandler>()
                        .SetupMoedeloXssProtection(XssValidationMode.RejectSuspiciousRequests)
                        .SetupMoedeloAuditTrail(true);

                    WebApiConfig.Register(config);
                }
            );
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalFilters.Filters.Add(Installer.GetInstance<MvcAuditFilter>());
        }

        protected void Application_End()
        {
            Installer.Dispose();
        }

        public static bool DebugMode
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
    }
}
