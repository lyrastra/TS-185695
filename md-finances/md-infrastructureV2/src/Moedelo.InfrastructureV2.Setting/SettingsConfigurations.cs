using System;
using System.Configuration;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.InfrastructureV2.Setting;

[InjectAsSingleton(typeof(ISettingsConfigurations))]
internal sealed class SettingsConfigurations : ISettingsConfigurations
{
    private const string ConsulEnvironmentName = "ConsulEnvironment";
    private const string ConsulEnvironmentKubernetesName = "CONSUL_ENVIRONMENT";
    private const string DefaultEnvironment = "dev";
    private const string DefaultShard = "0";
    private const string DefaultConfigName = "default";

    private readonly Lazy<SettingsConfig> lazyConfig = new (() => new SettingsConfig(
        Environment: Environment.GetEnvironmentVariable(ConsulEnvironmentKubernetesName, EnvironmentVariableTarget.Process) ??
                     Environment.GetEnvironmentVariable(ConsulEnvironmentKubernetesName, EnvironmentVariableTarget.Machine) ??
                     Environment.GetEnvironmentVariable(ConsulEnvironmentName, EnvironmentVariableTarget.Process) ??
                     Environment.GetEnvironmentVariable(ConsulEnvironmentName, EnvironmentVariableTarget.Machine) ??
                     DefaultEnvironment,
        Shard: DefaultShard,
        ConfigName: DefaultConfigName,
        Domain: GetAppSettings("Settings.Domain") ?? string.Empty,
        AppName: GetAppSettings("Settings.AppName") ?? string.Empty));

    public SettingsConfig Config => lazyConfig.Value;

    private static string GetAppSettings(string settingName)
    {
        try
        {
            return ConfigurationManager.AppSettings[settingName];
        }
        catch
        {
            return null;
        }
    }
}