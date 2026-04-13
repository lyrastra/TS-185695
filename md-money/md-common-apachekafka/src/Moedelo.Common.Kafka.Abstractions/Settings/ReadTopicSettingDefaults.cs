namespace Moedelo.Common.Kafka.Abstractions.Settings
{
    public static class ReadTopicSettingDefaults
    {
        public const int MaxPollIntervalMs = 300000;
        public const int SessionTimeoutMs = 10000;
        public const int FetchWaitMaxMs = 1000;
        public const int FetchErrorBackoffMs = 2000;
        public const int FetchMinBytes = 1;
        public const int FetchMaxBytes = 5242880;
        public const int QueuedMinMessages = 100;
    }
}
