namespace Moedelo.Changelog.Shared.Kafka.Abstractions
{
    public static class Topics
    {
        private const string Domain = "ChangeLog";
        
        public static class EntityState
        {
            public const string EntityName = nameof(EntityState);

            public static class Event
            {
                public static readonly string Topic = $"{Domain}.Event.{EntityName}";
            }
            
            public static class Command
            {
                public static readonly string Topic = $"{Domain}.Command.{EntityName}";
            }
        }
    }
}
