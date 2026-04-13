using Dapper;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess
{
    internal sealed class OutParameterReader : IOutParameterReader
    {
        private readonly DynamicParameters _dynamicParameters;

        public OutParameterReader(DynamicParameters dynamicParameters)
        {
            _dynamicParameters = dynamicParameters;
        }

        public T Read<T>(string paramName)
        {
            return _dynamicParameters.Get<T>(paramName);
        }
    }
}