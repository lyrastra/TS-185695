using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Moedelo.CommonV2.Extensions.System
{
    public static class AssemblyExt
    {
        public static string GetResource(this Assembly assembly, string resourceName)
        {
            var assemblyName = assembly.GetName().Name;
            using (Stream stream = assembly.GetManifestResourceStream($"{assemblyName}.{resourceName}"))
            {
                if (stream == null)
                {
                    throw new Exception($"Resource {resourceName} not found in assembly {assemblyName}");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        
        public static Task<string> GetResourceAsync(this Assembly assembly, string resourceName)
        {
            var assemblyName = assembly.GetName().Name;
            using (Stream stream = assembly.GetManifestResourceStream($"{assemblyName}.{resourceName}"))
            {
                if (stream == null)
                {
                    throw new Exception($"Resource {resourceName} not found in assembly {assemblyName}");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEndAsync();
                }
            }
        }
        
        public static async Task<byte[]> GetResourceAsByteArrayAsync(this Assembly assembly, string resourceName)
        {
            var assemblyName = assembly.GetName().Name;
            using (Stream stream = assembly.GetManifestResourceStream($"{assemblyName}.{resourceName}"))
            {
                if (stream == null)
                {
                    throw new Exception($"Resource {resourceName} not found in assembly {assemblyName}");
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream).ConfigureAwait(false);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
