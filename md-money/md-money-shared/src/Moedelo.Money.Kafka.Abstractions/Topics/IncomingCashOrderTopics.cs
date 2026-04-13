namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    /*
     * После запуска топика в Production не изменяйте его название
     */
    public static partial class CashOrderTopics
    {
        public static class RetailRevenue
        {
            public static class Event
            {
                private const string prefix = "Money.CashOrders.Event.RetailRevenue";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }
    }
}