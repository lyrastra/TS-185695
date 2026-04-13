namespace Moedelo.TaxPostings.Kafka.Abstractions
{
    public static partial class TaxPostingsTopics
    {
        public static partial class Usn
        {
            public static readonly string EntityName = $"{Prefix}.Usn";

            public static class Command
            {
                public static readonly string Topic = $"{EntityName}.Command";
            }
        }
    }
}
