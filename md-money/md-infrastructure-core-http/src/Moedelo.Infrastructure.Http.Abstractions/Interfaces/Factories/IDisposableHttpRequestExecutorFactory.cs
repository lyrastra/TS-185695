using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Infrastructure.Http.Abstractions.Interfaces.Factories
{
    public interface IDisposableHttpRequestExecutorFactory
    {
        IDisposableHttpRequestExecutor Create(HttpRequestExecutionSettings settings);
    }
}