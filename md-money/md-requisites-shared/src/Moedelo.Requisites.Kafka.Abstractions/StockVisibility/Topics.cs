namespace Moedelo.Requisites.Kafka.Abstractions.StockVisibility
{
    public class Topics
    {
        private const string DomainName = "Requisites";
        
        //Entity
        public static class StockVisibilityEntity
        {
            public const string EntityName = "StockVisibility";

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
    }
}