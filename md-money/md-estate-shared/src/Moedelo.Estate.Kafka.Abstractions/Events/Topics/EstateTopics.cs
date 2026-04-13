namespace Moedelo.Estate.Kafka.Abstractions.Events.Topics
{
    public class EstateTopics
    {
        public static class InventoryCard
        {
            public const string CUD = "Estate.InventoryCard.Event.CUD";
            public static string EntityName = "InventoryCard";
        }
    }
}