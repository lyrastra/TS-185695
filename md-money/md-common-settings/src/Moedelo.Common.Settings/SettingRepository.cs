using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Settings.Extensions;
using Moedelo.Common.Settings.Crypto;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Settings;

[InjectAsSingleton(typeof(ISettingRepository))]
internal sealed class SettingRepository : ISettingRepository, IDisposable
{
    private static readonly IReadOnlyDictionary<string, string> EmptySettings = new Dictionary<string, string>();

    private IReadOnlyDictionary<string, string> appSettings = EmptySettings;
    private IReadOnlyDictionary<string, string> domainCommonSettings = EmptySettings;
    private IReadOnlyDictionary<string, string> commonSettings = EmptySettings;
    private IReadOnlyDictionary<string, string> globalSettings = EmptySettings;

    private readonly ISettingsConfigurations configurations;
    private readonly IConsulCatalogWatcher catalogWatcher;
    private readonly ILogger logger;

    public SettingRepository(
        ILogger<SettingRepository> logger,
        ISettingsConfigurations configurations,
        ILoggingSettings loggingSettings,
        IConsulCatalogWatcher catalogWatcher)
    {
        this.configurations = configurations;
        this.catalogWatcher = catalogWatcher;
        this.logger = logger;
        loggingSettings.MinLogLevelSetting = this.GetEnum("LogLevel", LogLevel.Information);

        catalogWatcher.OnError += OnError;
        catalogWatcher.OnRestoreAfterError += OnRestoreAfterError;

        // тут бы как-нибудь получить токен остановки приложения
        var appStoppingToken = CancellationToken.None;
        var initialDataLoadingTasks = StartDataLoadingTasks(appStoppingToken).ToArray();
        Task.WaitAll(initialDataLoadingTasks);
    }

    public void Dispose()
    {
        catalogWatcher.OnRestoreAfterError -= OnRestoreAfterError;
        catalogWatcher.OnError -= OnError;
    }

    public SettingValue Get(string settingName)
    {
        return new SettingValue(settingName, GetValueIfLoadedOrDefault);
    }

    private void OnRestoreAfterError(int runningErrorsCount, string keyPath)
    {
        logger.LogRestoreAfterError(keyPath, runningErrorsCount);
    }

    private void OnError(int runningErrorsCount, string keyPath, string lastErrorMessage)
    {
        var needToLog = runningErrorsCount is > 1 and <= 10 || runningErrorsCount % 10 == 0;

        if (needToLog)
        {
            logger.LogLoadingError(keyPath, runningErrorsCount, lastErrorMessage);
        }
    }

    private IEnumerable<Task> StartDataLoadingTasks(CancellationToken cancellationToken)
    {
        var config = configurations.Config;

        if (string.IsNullOrWhiteSpace(config.Domain) == false)
        {
            if (string.IsNullOrWhiteSpace(config.AppName) == false)
            {
                var appDirPath = $"{config.Environment}/{config.Domain}/{config.AppName}";
                yield return catalogWatcher.LoadAndWatchDirectoryAsync(appDirPath,
                    settings => OnApplicationSettingsLoaded(appDirPath, settings),
                    cancellationToken);
            }

            var domainDirPath = $"{config.Environment}/{config.Domain}/common";
            yield return catalogWatcher.LoadAndWatchDirectoryAsync(
                domainDirPath,
                settings => OnDomainSettingsLoaded(domainDirPath, settings),
                cancellationToken);
        }

        var commonDirPath = $"{config.Environment}/common";
        yield return catalogWatcher.LoadAndWatchDirectoryAsync(
            commonDirPath,
            settings => OnCommonSettingsLoaded(commonDirPath, settings),
            cancellationToken);

        var globalDirPath = $"{config.Environment}/global";
        yield return catalogWatcher.LoadDirectoryOnceAsync(
            globalDirPath,
            settings => OnGlobalSettingsLoaded(globalDirPath, settings),
            cancellationToken);
    }

    private void OnApplicationSettingsLoaded(
        string consulPath,
        IReadOnlyCollection<KeyValuePair<string, string>> settings)
    {
        appSettings = MapToDictionary(settings);
        logger.LogApplicationSettingsAreLoaded(consulPath, appSettings, configurations.Config);
    }

    private void OnDomainSettingsLoaded(
        string consulPath,
        IReadOnlyCollection<KeyValuePair<string, string>> settings)
    {
        domainCommonSettings = MapToDictionary(settings);
        logger.LogDomainSettingsAreLoaded(consulPath, domainCommonSettings, configurations.Config);
    }

    private void OnCommonSettingsLoaded(string consulPath,
        IReadOnlyCollection<KeyValuePair<string, string>> settings)
    {
        commonSettings = MapToDictionary(settings);
        logger.LogCommonSettingsAreLoaded(consulPath, commonSettings, configurations.Config);
    }

    private void OnGlobalSettingsLoaded(string consulPath,
        IReadOnlyCollection<KeyValuePair<string, string>> settings)
    {
        globalSettings = MapToDictionary(settings);
        logger.LogGlobalSettingsAreLoaded(consulPath, globalSettings, configurations.Config);
    }

    private IReadOnlyDictionary<string, string> MapToDictionary(IEnumerable<KeyValuePair<string, string>> settings)
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
                keyValuePair => keyValuePair.Key.RemovePrefix(prefix),
                keyValuePair => keyValuePair.Value.AddAppNameToDatabaseConnectionString(appName));
    }

    private string GetValueIfLoadedOrDefault(string settingName)
    {
        return ResolveSettingValue(settingName);
    }

    private string ResolveSettingValue(string settingName)
    {
        var value = GetValueOrDefault(appSettings, settingName) ??
                    GetValueOrDefault(domainCommonSettings, settingName) ??
                    GetValueOrDefault(commonSettings, settingName) ??
                    GetValueOrDefault(globalSettings, settingName);

        return Decryptor.TryDecrypt(value, out var decryptedValue) ? decryptedValue : value;
    }

    private static T GetValueOrDefault<T>(IReadOnlyDictionary<string, T> source, string key)
    {
        return source.TryGetValue(key, out var value) ? value : default(T);
    }
}
