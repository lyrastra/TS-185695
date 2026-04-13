using System;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Exceptions
{
    public class ScriptNotFoundException : Exception
    {
        public ScriptNotFoundException(string assemblyName, string scriptPath) : base(
            $"Script {scriptPath} not found in assembly {assemblyName}")
        {
        }
    }
}