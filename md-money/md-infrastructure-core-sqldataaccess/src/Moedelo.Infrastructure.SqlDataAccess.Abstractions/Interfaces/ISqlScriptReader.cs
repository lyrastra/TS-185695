using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Exceptions;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;

public interface ISqlScriptReader
{
    /// <exception cref="ScriptNotFoundException"></exception>
    string Get<T>(T dao, string scriptPath);
}
