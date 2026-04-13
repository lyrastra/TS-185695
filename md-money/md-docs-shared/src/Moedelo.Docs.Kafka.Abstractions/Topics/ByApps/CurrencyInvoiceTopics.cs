namespace Moedelo.Docs.Kafka.Abstractions.Topics.ByApps
{
    public static class CurrencyInvoiceTopics
    {
        /*
        * После запуска топика в Production не изменяйте его название
        */

        public static class Sales
        {
            public static class Event
            {
                public const string CUD = "CurrencyInvoice.Sales.Event.CUD";
            }
            public static class Command
            {
                public const string RecalculateExchangeDifference = "CurrencyInvoice.Sales.Command.RecalculateExchangeDifference";
            }
        }
        
        public static class Purchase
        {
            public static class Event
            {
                public const string CUD = "CurrencyInvoice.Purchase.Event.CUD";
            }
        }
    }
}