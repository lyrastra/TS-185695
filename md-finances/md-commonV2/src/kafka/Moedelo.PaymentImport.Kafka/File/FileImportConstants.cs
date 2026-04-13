namespace Moedelo.PaymentImport.Kafka.File
{
    public static class FileImportConstants
    {
        public const string EntityName = "File";

        public static class Event
        {
            public static readonly string Topic = $"PaymentOrderImport.Event.{EntityName}";
        }
    }
}
