using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace Moedelo.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        // ReSharper disable once InconsistentNaming
        public static IServiceCollection RegisterByDIAttribute(this IServiceCollection services,
            string assemblySearchPattern)
        {
            var assemblyPath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!).LocalPath;
            services.RegisterByDIAttribute(assemblyPath, assemblySearchPattern);

            return services;
        }

        // ReSharper disable once InconsistentNaming
        public static void RegisterByDIAttribute(this IServiceCollection services, string assemblyPath, string assemblySearchPattern)
        {
            var installer = new DIInstaller(services);
            installer.RegisterByDIAttribute(assemblyPath, assemblySearchPattern);

            installer.Initialize();
        }

        // ReSharper disable once InconsistentNaming
        public static void RegisterByDIAttribute(this IServiceCollection services, params Assembly[] assemblyList)
        {
            var installer = new DIInstaller(services);
            installer.RegisterByDIAttribute(assemblyList);
            installer.Initialize();
        }
    }
}
