using System;

namespace Moedelo.AccPostings.Kafka.Abstractions
{
    public class AccPostingsTopics
    {
        public static class Commands
        {
            private const string Prefix = "AccPostings.Command";
            
            public static readonly string PostingsV2OD = $"{Prefix}.Postings.V2.OD";
        }
    }
}
