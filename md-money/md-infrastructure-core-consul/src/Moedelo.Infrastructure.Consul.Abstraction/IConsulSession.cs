using System;

namespace Moedelo.Infrastructure.Consul.Abstraction;

public interface IConsulSession : IAsyncDisposable
{
    string SessionId { get; }
}
