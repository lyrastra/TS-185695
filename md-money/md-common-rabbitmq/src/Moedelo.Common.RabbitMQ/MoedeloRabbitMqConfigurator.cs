using System;
using Moedelo.Common.RabbitMQ.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.RabbitMQ
{
    [InjectAsSingleton(typeof(IMoedeloRabbitMqConfigurator))]
    internal sealed class MoedeloRabbitMqConfigurator : IMoedeloRabbitMqConfigurator
    {
        private readonly SettingValue useMachineNameAsQueuePrefixSetting;
        private readonly SettingValue queuePrefixSetting;
        private readonly SettingValue busConfigurationSetting;

        public MoedeloRabbitMqConfigurator(ISettingRepository settingRepository)
        {
            this.useMachineNameAsQueuePrefixSetting = settingRepository.Get("EventBusQueuePrefixUseMachineName");
            this.queuePrefixSetting = settingRepository.Get("EventBusQueuePrefix");
            this.busConfigurationSetting = settingRepository.Get("EventBusConfiguration");
        }

        public string GetExchangeNamePrefix()
        {
            var queuePrefix = queuePrefixSetting.Value;

            if (!string.IsNullOrEmpty(queuePrefix))
            {
                return queuePrefix;
            }

            if (useMachineNameAsQueuePrefixSetting.GetBoolValueOrDefault(false))
            {
                return Environment.MachineName;
            }

            throw new Exception($"Парадокс настроек: ожидается, что либо выставлено непустое значение для настройки {queuePrefixSetting.Name}, " +
                                $"либо выставлено значение true для настройки {useMachineNameAsQueuePrefixSetting.Name}");

        }

        public string GetConnection()
        {
            var config = busConfigurationSetting.Value;

            if (string.IsNullOrEmpty(config))
            {
                throw new ArgumentException($"{busConfigurationSetting.Name} cannot be null or empty");
            }

            return config;
        }
    }
}
