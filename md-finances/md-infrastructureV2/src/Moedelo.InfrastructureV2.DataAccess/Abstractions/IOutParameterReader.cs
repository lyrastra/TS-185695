namespace Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;

public interface IOutParameterReader
{
    T Read<T>(string paramName);
}