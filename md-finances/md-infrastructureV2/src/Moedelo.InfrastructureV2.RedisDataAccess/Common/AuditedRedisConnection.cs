using System.Text.RegularExpressions;
using IRedisConnectionV2 = Moedelo.InfrastructureV2.Domain.Models.Redis.RedisConnection;
using Moedelo.Infrastructure.Redis.Abstractions.Models;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Common
{
    internal sealed class AuditedRedisConnection : IRedisConnection, IRedisConnectionV2
    {
        internal IRedisConnectionV2 ObscuredConnectionInfo { get; }

        public AuditedRedisConnection(string cnn, int dbNumber)
        {
            ConnectionString = cnn;
            DbNumber = dbNumber;
            
            ObscuredConnectionInfo = new RedisConnectionImpl
            {
                ConnectionString = ObscureConnectionString(cnn),
                DbNumber = dbNumber 
            };
        }

        private static readonly Regex password = new Regex($"password=.*?,", RegexOptions.Compiled);

        private static string ObscureConnectionString(string connectionString)
        {
            return password.Replace(connectionString, "password=***,");
        }

        private class RedisConnectionImpl : IRedisConnectionV2
        {
            public string ConnectionString { get; set; }
            public int DbNumber { get; set; }
        }

        public string ConnectionString { get; private set; }
        public int DbNumber { get; private set; }
        string IRedisConnectionV2.ConnectionString => this.ConnectionString;
        int IRedisConnectionV2.DbNumber => this.DbNumber;
    }
}
