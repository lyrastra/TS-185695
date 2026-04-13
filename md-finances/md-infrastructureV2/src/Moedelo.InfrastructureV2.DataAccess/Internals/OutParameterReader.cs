using Dapper;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;

namespace Moedelo.InfrastructureV2.DataAccess.Internals;

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
