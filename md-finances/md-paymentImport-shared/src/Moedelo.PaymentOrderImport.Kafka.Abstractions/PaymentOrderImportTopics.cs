namespace Moedelo.PaymentOrderImport.Kafka.Abstractions
{
    public static class ImportTopics
    {
        public static string Domain = "PaymentOrderImport";

        /// <summary>
        /// Импорт выписки
        /// </summary>
        public static class Movement
        {
            public static string SubDomain = nameof(Movement);

            /// <summary>
            /// Статус импорта выписки
            /// </summary>
            public static class Status
            {
                public static string EntityName = nameof(Status);

                public static class Event
                {
                    public static readonly string Topic = $"{Domain}.{SubDomain}.Event.{EntityName}";
                }
            }

            /// <summary>
            /// Статус импорта документа из выписки
            /// </summary>
            public static class Document
            {
                public static string EntityName = nameof(Document);

                public static class Event
                {
                    public static readonly string Topic = $"{Domain}.{SubDomain}.Event.{EntityName}";
                }
            }
        }

        /// <summary>
        /// Импорт для пользователя
        /// </summary>
        public static class ImportForUser
        {
            public static string SubDomain = "ImportForUser";

            /// <summary>
            /// Статус импорта документа для пользователя
            /// </summary>
            public static class Document
            {
                public static string EntityName = nameof(Document);

                public static class Event
                {
                    public static readonly string Topic = $"{Domain}.{SubDomain}.Event.{EntityName}";
                }
            }
        }

        /// <summary>
        /// Команды для правил импорта
        /// </summary>
        public static class Rule
        {
            public static string SubDomain = "Rules";
            public static string EntityName = nameof(Rule);

            public static class Commands
            {
                public static readonly string Topic = $"{Domain}.{SubDomain}.Command.{EntityName}";
            }
        }
    }
}
