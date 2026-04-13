namespace Moedelo.Infrastructure.Redis.Abstractions.Models
{
    public interface IRedisConnection
    {
        string ConnectionString { get; }
        int DbNumber { get; }
    }
}
