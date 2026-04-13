using System;
using System.Configuration;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Injection.LightInject.Web;
using Moedelo.InfrastructureV2.Logging;
using Topshelf;

namespace Moedelo.Finances.EventBusHandler
{
    public class Program
    {
        private const string TAG = nameof(Program);
        private const string AppNameSettingsKey = "appName";

        private static readonly string AppName = ConfigurationManager.AppSettings[AppNameSettingsKey];
        private static readonly WebDiInstaller Installer = new WebDiInstaller(new Logger());

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            Installer.Initialize();

            if (args.Length > 0 && args[0] == "--debug")
            {
                DebugStart();
                return;
            }

            HostFactory.Run(x =>
            {
                x.EnableShutdown();
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.SetDescription($"{AppName} Service");
                x.SetDisplayName($"{AppName} Service");
                x.SetServiceName(AppName);
                x.Service<EventBusHandler>(s =>
                {
                    s.ConstructUsing(name => Installer.GetInstance<EventBusHandler>());
                    s.WhenStarted(tc => tc.ProcessStart());
                    s.WhenStopped(tc =>
                    {
                        tc.ProcessStop();
                        Installer.Dispose();
                    });
                });
                
                x.EnableServiceRecovery(r =>
                {
                    r.RestartService(0);
                });
            });
        }

        private static void DebugStart()
        {
            var handler = Installer.GetInstance<EventBusHandler>();
            handler.ProcessStart();
            Console.WriteLine(@"Press any key to quit");
            Console.ReadKey();
            handler.ProcessStop();
            Installer.Dispose();
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var logger = Installer.GetInstance<ILogger>();
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                logger.Error(TAG, "Unhandled exception in service", exception);
            }
            else
            {
                logger.Fatal(TAG, "Unhandled error of unknown type: " + e.ExceptionObject);
            }
        }
    }
}
