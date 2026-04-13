using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Internal
{
    internal static class MongoConnectionHelper
    {
        internal static MongoConnection GetMongoConnection(
            SettingValue mongoConnectionStringSetting,
            SettingValue mongoDatabaseNameSetting)
        {
            var mongoConnectionString = mongoConnectionStringSetting.Value;
            var mongoDatabaseName = mongoDatabaseNameSetting.Value;

            return string.IsNullOrWhiteSpace(mongoDatabaseName)
                ? new MongoConnection(mongoConnectionString)
                : new MongoConnection(mongoConnectionString, mongoDatabaseName);
        }

        internal static MongoCollectionConnection GetMongoCollectionConnection(
            SettingValue mongoConnectionStringSetting,
            SettingValue mongoDatabaseNameSetting,
            SettingValue mongoCollectionNameSetting)
        {
            var mongoConnectionString = mongoConnectionStringSetting.Value;
            var mongoDatabaseName = mongoDatabaseNameSetting.Value;
            var mongoCollectionName = mongoCollectionNameSetting.Value;

            return string.IsNullOrWhiteSpace(mongoDatabaseName)
                ? new MongoCollectionConnection(mongoConnectionString, mongoCollectionName)
                : new MongoCollectionConnection(mongoConnectionString, mongoDatabaseName, mongoCollectionName);
        }
    }
}