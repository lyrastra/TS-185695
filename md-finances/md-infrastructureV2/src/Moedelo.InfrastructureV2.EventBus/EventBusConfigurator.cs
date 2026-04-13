using System;
using System.Configuration;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.InfrastructureV2.EventBus
{
    [InjectAsSingleton(typeof(IEventBusConfigurator))]
    internal sealed class EventBusConfigurator : IEventBusConfigurator
    {
        private readonly SettingValue useMachineNameAsQueuePrefixSetting;
        private readonly SettingValue queuePrefixSetting;
        private readonly SettingValue busConfigurationSetting;

        public EventBusConfigurator(ISettingRepository settingRepository)
        {
            this.useMachineNameAsQueuePrefixSetting = settingRepository.Get("EventBusQueuePrefixUseMachineName");
            this.queuePrefixSetting = settingRepository.Get("EventBusQueuePrefix");
            this.busConfigurationSetting = settingRepository.Get("EventBusConfiguration");
        }

        public string GetNamePrefixConfig()
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

        public string GetEventBusConfig()
        {
            var config = busConfigurationSetting.Value;

            if (string.IsNullOrEmpty(config))
            {
                throw new ArgumentException($"{busConfigurationSetting.Name} cannot be null or empty");
            }
            
            return config;
        }

        public string GetClientId()
        {
            var appName = ConfigurationManager.AppSettings["appName"];
            
            if (string.IsNullOrEmpty(appName))
            {
                throw new ConfigurationErrorsException("appName is not defined");
            }
            
            return appName;
        }
    }
}
