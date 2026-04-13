namespace Moedelo.Docs.Kafka.Abstractions.Topics.ByApps
{
    public static class InvoiceTopics
    {
        /*
        * После запуска топика в Production не изменяйте его название
        */

        public static class Sales
        {
            public static class Event
            {
                public const string CUD = "Invoice.Sales.Event.CUD";
            }
        }
    }
}