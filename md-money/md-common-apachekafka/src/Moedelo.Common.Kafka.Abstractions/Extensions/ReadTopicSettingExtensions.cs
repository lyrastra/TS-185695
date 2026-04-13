using Moedelo.Common.Kafka.Abstractions.Entities;
using Moedelo.Common.Kafka.Abstractions.Settings;

namespace Moedelo.Common.Kafka.Abstractions.Extensions
{
    internal static class ReadTopicSettingExtensions
    {
        internal static void SetOptionalSettings(this KafkaConsumerSettings settings, OptionalReadSettings optional)
        {
            if (optional == null)
            {
                return;
            }

            if (optional.FetchWaitMaxMs.HasValue)
            {
                settings.FetchWaitMaxMs = optional.FetchWaitMaxMs.Value;
            }

            if (optional.FetchErrorBackoffMs.HasValue)
            {
                settings.FetchErrorBackoffMs = optional.FetchErrorBackoffMs.Value;
            }

            if (optional.FetchMinBytes.HasValue)
            {
                settings.FetchMinBytes = optional.FetchMinBytes.Value;
            }

            if (optional.FetchMaxBytes.HasValue)
            {
                settings.FetchMaxBytes = optional.FetchMaxBytes.Value;
            }

            if (optional.QueuedMinMessages.HasValue)
            {
                settings.QueuedMinMessages = optional.QueuedMinMessages.Value;
            }

            if (optional.SessionTimeoutMs.HasValue)
            {
                settings.SessionTimeoutMs = optional.SessionTimeoutMs.Value;
            }

            if (optional.MaxPollIntervalMs.HasValue)
            {
                settings.MaxPollIntervalMs = optional.MaxPollIntervalMs.Value;
            }

            if (optional.UseExecutionContext.HasValue)
            {
                settings.UseContext = optional.UseExecutionContext.Value;
            }
        }

        internal static void SetDefaultSettingsIfNotSet(this KafkaConsumerSettings settings)
        {
            if (settings.FetchWaitMaxMs.HasValue == false)
            {
                settings.FetchWaitMaxMs = ReadTopicSettingDefaults.FetchWaitMaxMs;
            }

            if (settings.FetchErrorBackoffMs.HasValue == false)
            {
                settings.FetchErrorBackoffMs = ReadTopicSettingDefaults.FetchErrorBackoffMs;
            }

            if (settings.FetchMinBytes.HasValue == false)
            {
                settings.FetchMinBytes = ReadTopicSettingDefaults.FetchMinBytes;
            }

            if (settings.FetchMaxBytes.HasValue == false)
            {
                settings.FetchMaxBytes = ReadTopicSettingDefaults.FetchMaxBytes;
            }

            if (settings.QueuedMinMessages.HasValue == false)
            {
                settings.QueuedMinMessages = ReadTopicSettingDefaults.QueuedMinMessages;
            }

            if (settings.SessionTimeoutMs.HasValue == false)
            {
                settings.SessionTimeoutMs = ReadTopicSettingDefaults.SessionTimeoutMs;
            }

            if (settings.MaxPollIntervalMs.HasValue == false)
            {
                settings.MaxPollIntervalMs = ReadTopicSettingDefaults.MaxPollIntervalMs;
            }
        }
    }
}
