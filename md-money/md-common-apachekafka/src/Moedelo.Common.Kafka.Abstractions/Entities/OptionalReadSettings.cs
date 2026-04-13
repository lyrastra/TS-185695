namespace Moedelo.Common.Kafka.Abstractions.Entities
{
    public sealed class OptionalReadSettings
    {
        public int? FetchWaitMaxMs { get; set; }

        public int? FetchErrorBackoffMs { get; set; }

        public int? FetchMinBytes { get; set; }

        public int? FetchMaxBytes { get; set; }

        public int? QueuedMinMessages { get; set; }

        public int? SessionTimeoutMs { get; set; }

        /// <summary>
        /// Максимальное время между соседними вызовами Consume.<br/>
        /// По сути, это максимальное время на обработку одного сообщения приложением.<br/>
        /// Единица измерения: <b>миллисекунды</b>.<br/>
        /// Если не указано, то будет использовано значение по умолчанию 300000 мс (== 300 секунд == 5 минут)
        /// </summary>
        /// <default>300000</default>
        public int? MaxPollIntervalMs { get; set; }

        /// <summary>
        /// Использовать или нет ExecutionContext
        /// </summary>
        public bool? UseExecutionContext { get; set; }
    }
}