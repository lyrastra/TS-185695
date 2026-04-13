namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyBankFee
{
    public static class CurrencyBankFeeConstants
    {
        public const string EntityName = "CurrencyBankFee";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
