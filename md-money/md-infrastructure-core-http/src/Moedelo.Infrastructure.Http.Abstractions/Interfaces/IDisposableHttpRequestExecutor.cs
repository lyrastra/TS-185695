using System;

namespace Moedelo.Infrastructure.Http.Abstractions.Interfaces
{
    public interface IDisposableHttpRequestExecutor : IHttpRequestExecuter, IDisposable
    {
    }
}