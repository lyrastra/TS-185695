using Dapper;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;

namespace Moedelo.Infrastructure.SqlDataAccess.Internals;

internal sealed class OutParameterReader : IOutParameterReader
{
    private readonly DynamicParameters dynamicParameters;

    public OutParameterReader(DynamicParameters dynamicParameters)
    {
        this.dynamicParameters = dynamicParameters;
    }

    public T Read<T>(string paramName)
    {
        return dynamicParameters.Get<T>(paramName);
    }
}