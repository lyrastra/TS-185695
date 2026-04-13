using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Exceptions;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface
{
    public interface ISqlScriptReader
    {
        /// <exception cref="ScriptNotFoundException"></exception>
        string Get<T>(T dao, string scriptPath);
    }
}