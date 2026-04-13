using Moedelo.Infrastructure.Redis.Abstractions.Models;
using StackExchange.Redis;

namespace Moedelo.Infrastructure.Redis
{
    internal interface IConnectionMultiplexerPool
    {
        ConnectionMultiplexer GetConnectionMultiplexer(IRedisConnection connection);
    }
}