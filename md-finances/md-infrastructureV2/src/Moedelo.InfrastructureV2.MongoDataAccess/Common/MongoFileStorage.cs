using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.DistributedFileSystem;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.MongoDataAccess.Internal;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Common
{
    [InjectAsSingleton]
    public sealed class MongoFileStorage : MongoFileStorageBase, IFileStorage
    {
        public MongoFileStorage(
            IInternalMongoFileStorage mongoFileStorage, 
            ISettingRepository settingRepository,
            IAuditTracer auditTracer)
            : base(
                mongoFileStorage,
                settingRepository.Get("MongoDbConnectionString"),
                SettingValue.CreateConstSettingValue(null),
                settingRepository.Get("MongoDbProductionMode"),
                auditTracer)
        {
        }
    }
}