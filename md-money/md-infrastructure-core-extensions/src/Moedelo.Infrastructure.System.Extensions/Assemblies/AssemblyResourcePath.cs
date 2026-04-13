using System.Reflection;

namespace Moedelo.Infrastructure.System.Extensions.Assemblies
{
    internal sealed class AssemblyResourcePath
    {
        public AssemblyResourcePath(Assembly assembly, string resourcePath)
        {
            Assembly = assembly;
            ResourcePath = resourcePath;
        }

        public Assembly Assembly { get; }

        public string ResourcePath { get; }
    }
}