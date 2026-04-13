using System.IO;
using System.Linq;
using System.Reflection;

namespace Moedelo.InfrastructureV2.Domain.Helpers;

public static class AssemblyHelper
{
    public static Assembly[] LoadAssembliesByPattern(string directoryPath, string searchPattern)
    {
        var files = Directory.GetFiles(
            directoryPath,
            $@"{searchPattern}.dll",
            SearchOption.TopDirectoryOnly);

        var assemblyList = files.Select(f => f
            .Replace(directoryPath, string.Empty)
            .Replace(".dll", string.Empty)
            .Replace("\\", string.Empty));

        return assemblyList.Select(Assembly.Load).ToArray();
    }
}