namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;

public interface IOutParameterReader
{
    T Read<T>(string paramName);
}