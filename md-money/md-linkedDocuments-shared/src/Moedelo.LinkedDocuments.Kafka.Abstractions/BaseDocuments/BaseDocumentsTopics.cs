namespace Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments
{
    public static class BaseDocumentsTopics
    {
        public static class Commands
        {
            private const string Prefix = "LinkedDocuments.BaseDocuments.Command";

            public static readonly string SetTaxStatusCommand = $"{Prefix}.BaseDocument.SetTaxStatus";
        }
    }
}
