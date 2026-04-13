namespace Moedelo.Stock.Kafka.Abstractions.Topics
{
    public static class StockTopics
    {
        public static string Domain = "Stock";

        public static class Product
        {
            public static string Entity = nameof(Product);

            public static class Event
            {
                public static string Topic = $"{Domain}.Event.{Entity}";
            }
        }

        public static class Operation
        {
            public static string Entity = nameof(Operation);

            public static class Event
            {
                public static string Topic = $"{Domain}.Event.{Entity}";
            }
        }

        public static class RequisitionWaybill
        {
            public static string Entity = nameof(RequisitionWaybill);

            public static class Event
            {
                public static string Topic = $"{Domain}.Event.{Entity}.CUD";
            }
        }
    }
}
