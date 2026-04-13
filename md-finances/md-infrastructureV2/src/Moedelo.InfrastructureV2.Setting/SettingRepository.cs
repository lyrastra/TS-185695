using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Consul;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.Setting.Crypto;
using Moedelo.InfrastructureV2.Setting.Extensions;

namespace Moedelo.InfrastructureV2.Setting;

[InjectAsSingleton(typeof(ISettingRepository))]
public sealed class SettingRepository : ISettingRepository
{
    private const string Tag = nameof(SettingRepository);
    private static readonly IReadOnlyDictionary<string, string> EmptySettings = new Dictionary<string, string>();

    private IReadOnlyDictionary<string, string> appSettings = EmptySettings;
    private IReadOnlyDictionary<string, string> domainCommonSettings = EmptySettings;
    private IReadOnlyDictionary<string, string> commonSettings = EmptySettings;
    private IReadOnlyDictionary<string, string> globalSettings = EmptySettings;
    private readonly ISettingsConfigurations configurations;
    private readonly ILogger logger;

    public SettingRepository(
        ISettingsConfigurations configurations,
        IConsulCatalogWatcher consulCatalogWatcher,
        ILogger logger)
    {
        this.configurations = configurations;
        this.logger = logger;

        LoadSettingsAtStart(consulCatalogWatcher);
    }

    private void LoadSettingsAtStart(IConsulCatalogWatcher consulCatalogWatcher)
    {
        var stopWatch = Stopwatch.StartNew();
        Task.WaitAll(StartSettingsWatchTasks(consulCatalogWatcher).ToArray());
        stopWatch.Stop();
        logger.Debug(Tag, "Начальная загрузка настроек закончена", extraData: new { Duration = stopWatch.Elapsed });
    }

    private IEnumerable<Task> StartSettingsWatchTasks(IConsulCatalogWatcher consulCatalogWatcher)
    {
        var config = configurations.Config;

        if (string.IsNullOrWhiteSpace(config.Domain) == false)
        {
            if (string.IsNullOrWhiteSpace(config.AppName) == false)
            {
                yield return consulCatalogWatcher.AddWatchCatalogAsync(
                    $"{config.Environment}/{config.Domain}/{config.AppName}/",
                    OnApplicationSettingsLoaded);
            }

            yield return consulCatalogWatcher.AddWatchCatalogAsync(
                $"{config.Environment}/{config.Domain}/common/",
                OnDomainSettingsLoaded);
        }

        yield return consulCatalogWatcher.AddWatchCatalogAsync(
            $"{config.Environment}/common/",
            OnCommonSettingsLoaded);
        yield return consulCatalogWatcher.AddStaticCatalogAsync(
            $"{config.Environment}/global/",
            OnGlobalSettingsLoaded);
    }

    private void OnApplicationSettingsLoaded(IReadOnlyCollection<KeyValuePair<string, string>> settings)
    {
        var config = configurations.Config;
        var newSettings = MapToDictionary(settings);
        logger.Info(Tag, "Загружены настройки уровня приложения",
            extraData: new {config.Domain, config.AppName, Settings = newSettings});
        appSettings = newSettings;
    }

    private void OnDomainSettingsLoaded(IReadOnlyCollection<KeyValuePair<string, string>> settings)
    {
        var newSettings = MapToDictionary(settings);
        logger.Info(Tag, "Загружены настройки уровня домена",
            extraData: new {configurations.Config.Domain, Settings = newSettings});
        domainCommonSettings = newSettings;
    }

    private void OnCommonSettingsLoaded(IReadOnlyCollection<KeyValuePair<string, string>> settings)
    {
        var newSettings = MapToDictionary(settings);
        logger.Info(Tag, "Загружены общие настройки", extraData: newSettings);
        commonSettings = newSettings;
    }

    private void OnGlobalSettingsLoaded(IReadOnlyCollection<KeyValuePair<string, string>> settings)
    {
        var newSettings = MapToDictionary(settings);
        logger.Info(Tag, "Загружены глобальные настройки", extraData: newSettings);
        globalSettings = newSettings;
    }

    private IReadOnlyDictionary<string, string> MapToDictionary(
        IReadOnlyCollection<KeyValuePair<string, string>> settings)
    {
        if (settings == null)
        {
            return EmptySettings;
        }

        var config = configurations.Config;
        var appName = config.AppName;
        var prefix = $"{config.ConfigName}:{config.Shard}:";

        return settings
            .ToDictionary(
                keyValuePair => RemovePrefix(keyValuePair.Key, prefix),
                keyValuePair => AddApplicationNameToMssqlConnectionString(keyValuePair.Value, appName));
    }

    private static string RemovePrefix(string settingName, string prefix)
    {
        if (settingName.StartsWith(prefix))
        {
            return settingName.Substring(prefix.Length);
        }

        return settingName;
    }

    private string CalcValue(string settingName)
    {
        var value = appSettings.GetValueOrNull(settingName) ??
                    domainCommonSettings.GetValueOrNull(settingName) ??
                    commonSettings.GetValueOrNull(settingName) ??
                    globalSettings.GetValueOrNull(settingName);

        return Decryptor.TryDecrypt(value, out var decryptedValue) ? decryptedValue : value;
    }

    public SettingValue Get(string settingName)
    {
        return new SettingValue(settingName, CalcValue);
    }

    private static string AddApplicationNameToMssqlConnectionString(string setting, string appName)
    {
        if (string.IsNullOrWhiteSpace(appName) || string.IsNullOrWhiteSpace(setting))
        {
            return setting;
        }

        return setting.StartsWith("Data Source=")
            ? $"{setting}; Application Name={appName}"
            : setting;
    }
}
