namespace Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;

public interface ISqlScriptReader
{
    /// <exception cref="ScriptNotFoundException"></exception>
    string Get<T>(T dao, string scriptPath);
}