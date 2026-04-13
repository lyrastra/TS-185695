using System;
using System.IO;
using System.Reflection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.InfrastructureV2.Injection.Lightinject
{
    /// <summary>
    /// Контейнер для использования в не-Web приложениях (например, консолях)
    /// </summary>
    public class AppAutoDiInstaller : BaseAutoDiInstaller
    {
        public AppAutoDiInstaller(ILogger logger, Assembly callingAssembly = null)
            : base(logger, callingAssembly ?? Assembly.GetCallingAssembly())
        {
        }

        protected override string AssemblyPath =>
            new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
    }
}
