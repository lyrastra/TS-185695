using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Mongo;
using Moedelo.InfrastructureV2.Domain.Models.Mongo;
using MongoDB.Bson.Serialization;

namespace Moedelo.InfrastructureV2.MongoDataAccess
{
    [InjectAsSingleton]
    public class MongoDbMapper : IMongoDbMapper
    {
        public void Map<T>() where T : IMongoObject
        {
            BsonClassMap.RegisterClassMap<T>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
            });
        }
    }
}
