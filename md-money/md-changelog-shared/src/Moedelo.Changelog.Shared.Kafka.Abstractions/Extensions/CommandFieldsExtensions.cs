namespace Moedelo.Changelog.Shared.Kafka.Abstractions.Extensions
{
    public static class CommandFieldsExtensions
    {
        public static string GetCommandKey(this EntityStateSaveCommandFields fields)
        {
            return $"{fields.FirmId}::{fields.EntityType.ToString()}::{fields.EntityId}";
        }
        
        public static string GetCommandKey(this ExplicitChangesSaveCommandFields fields)
        {
            return $"{fields.FirmId}::{fields.EntityType.ToString()}::{fields.EntityId}";
        }
    }
}
