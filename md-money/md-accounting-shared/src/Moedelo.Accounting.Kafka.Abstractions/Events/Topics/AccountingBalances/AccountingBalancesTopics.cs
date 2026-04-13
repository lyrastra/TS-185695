namespace Moedelo.Accounting.Kafka.Abstractions.Events.Topics.AccountingBalances
{
    public class AccountingBalancesTopics
    {
        private static readonly string Domain = "Accounting";
        private static readonly string Subdomain = "AccountingBalances";

        public static class Event
        {
            public static readonly string EntityName = "MinDateChanged";
            public static readonly string Topic = $"{Domain}.{Subdomain}.Event.{EntityName}";
        }
    }
}