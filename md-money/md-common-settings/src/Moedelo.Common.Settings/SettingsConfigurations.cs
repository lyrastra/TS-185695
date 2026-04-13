using Microsoft.Extensions.Configuration;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.IO;

namespace Moedelo.Common.Settings
{
    [InjectAsSingleton(typeof(ISettingsConfigurations))]
    internal sealed class SettingsConfigurations : ISettingsConfigurations
    {
        private const string AppSettingsFileName = "appsettings.json"; 

        private const string ConsulEnvironmentName = "ConsulEnvironment";
        private const string ConsulEnvironmentKubernetName = "CONSUL_ENVIRONMENT";
        private const string DefaultEnvironment = "dev";
        private const string DefaultShard = "0";
        private const string DefaultConfigName = "default";

        private IConfiguration Configuration { get; }

        public SettingsConfigurations()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingsFileName)
                .AddEnvironmentVariables("MD_APP_SETTING_")
                .Build();
        }

        public SettingsConfig Config =>
            new SettingsConfig
            {
                Environment = Environment.GetEnvironmentVariable(ConsulEnvironmentKubernetName, EnvironmentVariableTarget.Process) ??
                              Environment.GetEnvironmentVariable(ConsulEnvironmentKubernetName, EnvironmentVariableTarget.Machine) ??
                              Environment.GetEnvironmentVariable(ConsulEnvironmentName, EnvironmentVariableTarget.Process) ??
                              Environment.GetEnvironmentVariable(ConsulEnvironmentName, EnvironmentVariableTarget.Machine) ??
                              DefaultEnvironment,
                Shard = DefaultShard,
                ConfigName = DefaultConfigName,
                Domain = GetNotEmptyAppSetting("Settings.Domain"),
                AppName = GetNotEmptyAppSetting("Settings.AppName"),
                AuditTrailAppName = GetAppSettingOrNull("Settings.AuditTrailAppName")
            };
        
        private string GetNotEmptyAppSetting(string settingName)
        {
            var value = Configuration[settingName];

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    $"Настройка не может иметь пустое значение. Проверьте содержимое файла {AppSettingsFileName}",
                    settingName);
            }

            return value;
        }

        private string GetAppSettingOrNull(string settingName)
        {
            try
            {
                return Configuration[settingName];
            }
            catch
            {
                return null;
            }
        }
    }
}
