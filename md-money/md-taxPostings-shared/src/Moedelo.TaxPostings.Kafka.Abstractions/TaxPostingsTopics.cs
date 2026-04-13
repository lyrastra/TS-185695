namespace Moedelo.TaxPostings.Kafka.Abstractions
{
    public static partial class TaxPostingsTopics
    {
        private const string Prefix = "TaxPostings";

        public static class Commands
        {
            private static readonly string Prefix = $"{TaxPostingsTopics.Prefix}.Command";

            public static readonly string OsnoOD = $"{Prefix}.Postings.Osno.OD";

            public static readonly string IpOsnoOD = $"{Prefix}.Postings.IpOsno.OD";

            public static readonly string PatentOD = $"{Prefix}.Postings.Patent.OD";
        }
    }
}
