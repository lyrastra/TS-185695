namespace Moedelo.Docs.Shared.Kafka.Abstractions
{
    public static class DocsTopics
    {
        private const string DomainName = "Docs";
        
        /// <summary>
        /// Сущность "Накладная (Продажи)"
        /// Идентификатор события - BaseId документа
        /// </summary>
        public static class SaleWaybill
        {
            public const string EntityName = "SaleWaybill";
                
            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
        
        /// <summary>
        /// Сущность "УПД (Продажи)"
        /// Идентификатор события - BaseId документа
        /// </summary>
        public static class SaleUpd
        {
            public const string EntityName = "SaleUpd";
                
            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }

            public static class Command
            {
                public static readonly string Topic = $"{DomainName}.Command.{EntityName}";
            }
        }
        
        /// <summary>
        /// Сущность "Накладная (Покупки)"
        /// Идентификатор события - BaseId документа
        /// </summary>
        public static class PurchaseWaybill
        {
            public const string EntityName = "PurchaseWaybill";
                
            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
        
        /// <summary>
        /// Сущность "УПД (Покупки)"
        /// Идентификатор события - BaseId документа
        /// </summary>
        public static class PurchaseUpd
        {
            public const string EntityName = "PurchaseUpd";
                
            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
    }
}