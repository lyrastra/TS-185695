using System;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models
{
    public class MongoCollectionConnection : MongoConnection
    {
        public string CollectionName { get; }

        public MongoCollectionConnection(string connectionString, string databaseName, string collectionName) : base(connectionString, databaseName)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentException(nameof(collectionName));
            }

            CollectionName = collectionName;
        }
        
        public MongoCollectionConnection(string connectionString, string collectionName) : base(connectionString)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentException(nameof(collectionName));
            }

            CollectionName = collectionName;
        }
    }
}