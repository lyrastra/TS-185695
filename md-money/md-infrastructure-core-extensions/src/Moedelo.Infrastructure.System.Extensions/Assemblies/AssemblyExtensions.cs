using System.Collections.Concurrent;
using System.IO;
using System.Reflection;

namespace Moedelo.Infrastructure.System.Extensions.Assemblies
{
    public static class AssemblyExtensions
    {
        private static readonly ConcurrentDictionary<AssemblyResourcePath, string> Resources 
            = new ConcurrentDictionary<AssemblyResourcePath, string>(new AssemblyResourcePathEqualityComparer());

        public static string GetResourceString(this Assembly assembly, string resourcePath)
        {
            var assemblyResourcePath = new AssemblyResourcePath(assembly, resourcePath);

            var resource = Resources.GetOrAdd(assemblyResourcePath, ResourceFactory);

            return resource;
        }
        
        private static string ResourceFactory(AssemblyResourcePath assemblyResourcePath)
        {
            var resource = GetResourceStringFromAssembly(assemblyResourcePath.Assembly, assemblyResourcePath.ResourcePath);

            return resource;
        }

        private static string GetResourceStringFromAssembly(Assembly assembly, string resourcePath)
        {
            var assemblyName = assembly.GetName().Name;
            
            using (var stream = assembly.GetManifestResourceStream($"{assemblyName}.{resourcePath}"))
            {
                if (stream == null)
                {
                    throw new ResourceNotFoundException(assemblyName, resourcePath);
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}