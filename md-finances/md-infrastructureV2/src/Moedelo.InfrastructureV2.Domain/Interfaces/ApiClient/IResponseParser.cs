using System.IO;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;

public interface IResponseParser
{
    TResult Parse<TResult>(string response);
    TResult Parse<TResult>(Stream stream);
}