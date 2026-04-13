using System.Linq;
using System.Reflection;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.InfrastructureV2.Injection.Lightinject
{
    /// <summary>
    /// Класс, который автоматически регистрирует все зависимости из библиотек Moedelo.*
    /// </summary>
    public abstract class BaseAutoDiInstaller : DIInstaller
    {
        private readonly Assembly callingAssembly;

        protected BaseAutoDiInstaller(ILogger logger, Assembly callingAssembly) : base(logger)
        {
            this.callingAssembly = callingAssembly ?? Assembly.GetCallingAssembly();
        }
        
        /// <summary>
        /// путь до каталога c библиотеками
        /// </summary>
        protected abstract string AssemblyPath { get; }

        protected override void RegisterBehaviour()
        {
            var path = AssemblyPath;

            var assemblies = AssemblyHelper
                .LoadAssembliesByPattern(path, "Moedelo.*")
                .Concat([callingAssembly])
                .Distinct()
                .OrderBy(GetMoedeloAssemblyInjectionOrder)
                .ToArray();

            RegisterByDIAttribute(assemblies);
        }

        private static int GetMoedeloAssemblyInjectionOrder(Assembly assembly)
        {
            var assemblyName = assembly.GetName().Name;

            if (assemblyName.StartsWith("Moedelo.InfrastructureV2."))
            {
                return 0;
            }

            if (assemblyName.StartsWith("Moedelo.CommonV2."))
            {
                return 1;
            }

            if (assemblyName.EndsWith(".Client"))
            {
                return 2;
            }

            return 3;
        }
    }
}
