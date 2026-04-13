namespace Moedelo.BankIntegrations.Accounts.Kafka.Abstractions.RequestAccounts
{
    public static class Topics
    {
        private const string DomainName = "BankIntegrations";

        //BankIntegrations.Command.Accounts.Fill
        //Entity
        public static class AccountsRequestEntity
        {
            public const string EntityName = "Accounts";

            public static class Command
            {
                public static readonly string Topic = $"{DomainName}.Command.{EntityName}.Fill";
            }
        }
    }
}