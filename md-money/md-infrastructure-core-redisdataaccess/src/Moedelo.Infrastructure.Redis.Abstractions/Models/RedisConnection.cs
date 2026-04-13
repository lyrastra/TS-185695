namespace Moedelo.Infrastructure.Redis.Abstractions.Models
{
    public class RedisConnection: IRedisConnection
    {
        public string ConnectionString { get; }

        public int DbNumber { get; }

        public RedisConnection(
            string cnn,
            int dbNumber)
        {
            ConnectionString = cnn;
            DbNumber = dbNumber;
        }
    }
}