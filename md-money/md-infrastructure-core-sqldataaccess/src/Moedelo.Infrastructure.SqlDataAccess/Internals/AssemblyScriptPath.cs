using System.Reflection;

namespace Moedelo.Infrastructure.SqlDataAccess.Internals;

internal sealed class AssemblyScriptPath
{
    public AssemblyScriptPath(Assembly assembly, string scriptPath)
    {
        Assembly = assembly;
        ScriptPath = scriptPath;
    }

    public Assembly Assembly { get; }

    public string ScriptPath { get; }
}