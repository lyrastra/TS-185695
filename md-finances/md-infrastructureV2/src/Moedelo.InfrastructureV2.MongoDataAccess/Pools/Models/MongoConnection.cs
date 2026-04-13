using System;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models
{
    public class MongoConnection
    {
        public string ConnectionString { get; }

        public string DatabaseName { get; }

        public MongoConnection(string connectionString, string databaseName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new AggregateException(nameof(connectionString));
            }
            
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new AggregateException(nameof(databaseName));
            }
            
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }

        public MongoConnection(string connectionString) : this(connectionString, ParseDbName(connectionString))
        {
        }

        private static string ParseDbName(string connectionString)
        {
            var lastIndexOfQuestion = connectionString.LastIndexOf('?');
            var lastIndexOfSlash = connectionString.LastIndexOf('/') + 1;
            var maxLength = lastIndexOfQuestion == -1
                ? connectionString.Length
                : lastIndexOfQuestion;
            
            return connectionString.Substring(lastIndexOfSlash, maxLength - lastIndexOfSlash);
        }
    }
}