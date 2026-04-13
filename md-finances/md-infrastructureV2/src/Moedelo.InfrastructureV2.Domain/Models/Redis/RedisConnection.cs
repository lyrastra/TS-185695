namespace Moedelo.InfrastructureV2.Domain.Models.Redis;

public interface RedisConnection
{
    string ConnectionString { get; }

    int DbNumber { get; }
}