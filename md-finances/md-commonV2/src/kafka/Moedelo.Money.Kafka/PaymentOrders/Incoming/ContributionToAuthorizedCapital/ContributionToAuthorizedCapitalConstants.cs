using Moedelo.Money.Kafka.PaymentOrders.Incoming.ContributionToAuthorizedCapital.Commands;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    public static class ContributionToAuthorizedCapitalConstants
    {
        public const string EntityName = "ContributionToAuthorizedCapital";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
