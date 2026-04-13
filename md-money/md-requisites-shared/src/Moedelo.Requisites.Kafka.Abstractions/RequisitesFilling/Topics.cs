namespace Moedelo.Requisites.Kafka.Abstractions.RequisitesFilling
{
    public static class Topics
    {
        private const string Domain = "Requisites";

        public static class Inn
        {
            public const string EntityName = nameof(Inn);

            public static class Command
            {
                public static readonly string Topic = $"{Domain}.Command.{EntityName}.Fill";
            }
        }
    }
}