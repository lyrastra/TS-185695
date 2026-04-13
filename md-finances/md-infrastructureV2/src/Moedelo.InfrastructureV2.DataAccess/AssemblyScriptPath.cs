using System.Reflection;

namespace Moedelo.InfrastructureV2.DataAccess;

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
