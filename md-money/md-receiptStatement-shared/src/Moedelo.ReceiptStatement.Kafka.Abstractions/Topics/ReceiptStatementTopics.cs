namespace Moedelo.ReceiptStatement.Kafka.Abstractions.Topics
{

    public static class ReceiptStatementTopics
    {
        /*
        * После запуска топика в Production не изменяйте его название
        */

        public static class ReceiptStatement
        {
            public static class Event
            {
                private const string prefix = "ReceiptStatement.Event";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }
    }
}
