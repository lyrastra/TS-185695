using System;

namespace Moedelo.InfrastructureV2.DataAccess;

public class ScriptNotFoundException : Exception
{
    public ScriptNotFoundException(string assemblyName, string scriptPath) : base(
        $"Script {scriptPath} not found in assembly {assemblyName}")
    {
    }
}
