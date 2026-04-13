namespace Moedelo.Accounts.Kafka.Abstractions
{
    public static class Topics
    {
        private const string DomainName = "Account";

        /// <summary>
        /// Сущность "Аккаунт"
        /// Идентификатор события - идентификатор аккаунта
        /// </summary>
        public static class Account
        {
            public const string EntityName = "Account";

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            } 
        }

        /// <summary>
        /// Сущность "Пользователь"
        /// Идентификатор события - идентификатор пользователя
        /// </summary>
        public static class User
        {
            public const string EntityName = "User";
            
            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }

        /// <summary>
        /// Сущность "Фирма"
        /// Идентификатор события - идентификатор фирмы
        /// </summary>
        public static class Firm
        {
            public const string EntityName = "Firm";

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
        
        public static class EntityMapping
        {
            public const string EntityName = "EntityMapping";
            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }

        /// <summary>
        /// Псевдо-сущность (область) "Обслуживание"
        /// Идентификатор события - случайное число
        /// </summary>
        public static class FirmMaintenance
        {
            public const string EntityName = "FirmMaintenance";

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }

        /// <summary>
        /// Сущность "Доступ к фирме"
        /// Идентификатор события - идентификатор фирмы
        /// </summary>
        public static class FirmAccess
        {
            public const string EntityName = "FirmAccess";

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
    }
}