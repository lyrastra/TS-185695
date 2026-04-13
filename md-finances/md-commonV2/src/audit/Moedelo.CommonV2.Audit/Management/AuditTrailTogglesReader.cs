using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonV2.Audit.Extensions;
using Moedelo.CommonV2.Audit.Management.Domain;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonV2.Audit.Management;

[InjectAsSingleton(typeof(IAuditTrailTogglesReader))]
internal sealed class AuditTrailTogglesReader : IAuditTrailTogglesReader, IDisposable
{
    private const string Tag = nameof(AuditTrailTogglesReader);

    private readonly IAuditRedisDbExecuter auditRedisDbExecutor;
    private readonly IntSettingValue updateTimeoutSetting;
    private readonly IntSettingValue delayAfterUpdateErrorSetting;
    private readonly ILogger logger;
    private readonly CancellationTokenSource stateCheckingCancellationTokenSource;
    private readonly TaskCompletionSource<bool> firstSettingsLoadedTaskSource = new ();
    private readonly Task stateCheckingTask;
    private bool disposed;

    private TimeSpan UpdateTimeout => TimeSpan.FromSeconds(updateTimeoutSetting.Value); 
    private TimeSpan DelayAfterUpdateError => TimeSpan.FromSeconds(delayAfterUpdateErrorSetting.Value); 

    public AuditTrailTogglesReader(
        IAuditRedisDbExecuter auditRedisDbExecutor, 
        ISettingRepository settingRepository,
        ILogger logger)
    {
        const int defaultUpdateTimeoutSeconds = 5;
        const int defaultDelayAfterUpdateErrorSeconds = 30;

        stateCheckingCancellationTokenSource = new CancellationTokenSource();
        this.updateTimeoutSetting = settingRepository.GetInt("AuditTrailRefreshTogglesEverySeconds", defaultUpdateTimeoutSeconds);
        this.delayAfterUpdateErrorSetting = settingRepository.GetInt("AuditTrailWaitSecondsAfterRefreshFailure", defaultDelayAfterUpdateErrorSeconds);
        this.auditRedisDbExecutor = auditRedisDbExecutor;
        this.logger = logger;
        this.AppName = GetAppSettings("Settings.AuditTrailAppName")
                       ?? GetAppSettings("appName")
                       ?? string.Empty;
        Current = new AuditTrailToggles();

        stateCheckingTask = StartStateCheckingTask(settingRepository, stateCheckingCancellationTokenSource.Token);
    }

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

    public string AppName { get; }

    public Task WaitForReadyAsync()
    {
        return firstSettingsLoadedTaskSource.Task;
    }

    public AuditTrailToggles Current { get; private set; }

    private async Task SaveApplicationRunningMarkerToRedisAsync()
    {
        try
        {
            var runKey = KeysHelper.GetCommandKey(AppName, "run");

            await auditRedisDbExecutor.SetValueForKeyAsync(runKey, "run").ConfigureAwait(false);
        }
        catch(Exception exception)
        {
            logger.Error(Tag, "AuditTrail: не удалось инициализировать цикл отслеживания состояния. Аудит будет отключен", exception: exception);
            throw;
        }
    }

    private async Task StateCheckingInfiniteLoopAsync(CancellationToken cancellationToken)
    {
        var isFirstRun = true;
        var commandNames = new[]
        {
            CommandKeys.CommandEnable,
            CommandKeys.CommandApiHandler,
            CommandKeys.CommandOutgoingHttpRequest,
            CommandKeys.CommandDbQuery,
            CommandKeys.CommandInternalCode
        };

        var commandRedisKeysList = commandNames
            .Select(command => KeysHelper.GetCommandKey(AppName, command))
            .ToArray();

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var state = await GetApplicationAuditTrailSwitchersAsync(commandNames, commandRedisKeysList)
                        .ConfigureAwait(false);
                    logger.LogStateIfChanged(Tag, isFirstRun ? null : Current, state);
                    Current = state;
                    isFirstRun = false;
                    firstSettingsLoadedTaskSource.TrySetResult(true);
                }
                catch (FileNotFoundException exception)
                {
                    logger.Error(Tag,
                        "AuditTrail: не удалось проверить статус. Аудит будет отключен",
                        exception: exception);
                    // это фатальная ошибка, нет смысла продолжать
                    throw;
                }
                catch (Exception exception)
                {
                    logger.Error(Tag,
                        "AuditTrail: не удалось проверить статус. Аудит может работать некорректно",
                        exception: exception);
                    // подождём чуть больше, потому что явно что-то не работает, надо просто подождать,
                    // а не создавать дополнительную нагрузку
                    await Task.Delay(DelayAfterUpdateError, cancellationToken).ConfigureAwait(false);
                    // продолжаем пробовать
                }

                await Task.Delay(UpdateTimeout, cancellationToken).ConfigureAwait(false);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            ; // ignore
        }
    }

    private Task StartStateCheckingTask(ISettingRepository settingRepository, CancellationToken cancellationToken)
    {
        var isAuditManagementEnabled = GetAuditManagementEnabled(settingRepository);
        var isAppNameNotEmpty = string.IsNullOrWhiteSpace(AppName) == false;
        var canBeEnabled = isAuditManagementEnabled && isAppNameNotEmpty;

        if (canBeEnabled == false)
        {
            logger.Debug(Tag, "AuditTrail: отключен и не может быть включен без перезапуска", extraData: new { AppName, isAuditManagementEnabled, isAppNameNotEmpty });
            firstSettingsLoadedTaskSource.TrySetResult(false);
            return Task.CompletedTask;
        }

        return Task.Factory.StartNew(async () =>
        {
            logger.Debug(Tag, "AuditTrail: начало отслеживания состояния", extraData: new { AppName });
            await SaveApplicationRunningMarkerToRedisAsync().ConfigureAwait(false);
            await StateCheckingInfiniteLoopAsync(cancellationToken).ConfigureAwait(false);
        }, TaskCreationOptions.LongRunning).Unwrap();
    }

    private async Task<AuditTrailToggles> GetApplicationAuditTrailSwitchersAsync(
        IReadOnlyList<string> commands,
        IReadOnlyCollection<string> commandRedisKeysList)
    {
        //читаем команды напрямую по ключам
        var keyValues = await auditRedisDbExecutor
            .GetValueListByKeyListAsync(commandRedisKeysList)
            .ConfigureAwait(false);

        var newState = new AuditTrailToggles();

        for (var i = 0; i < keyValues.Count; ++i)
        {
            newState.SetEnabled(commands[i], keyValues[i]);
        }

        return newState;
    }

    private static bool GetAuditManagementEnabled(ISettingRepository settingRepository)
    {
        try
        {
            bool.TryParse(settingRepository.Get("AuditManagementEnabled").Value, out var enabled);
            return enabled;
        }
        catch
        {
            return false;
        }
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        if (!stateCheckingCancellationTokenSource.IsCancellationRequested)
        {
            stateCheckingCancellationTokenSource.Cancel();
            try
            {
                stateCheckingTask.Wait(TimeSpan.FromSeconds(1));
            }
            catch
            {
                /*ignore errors*/
            }
        }

        stateCheckingCancellationTokenSource.Dispose();
    }
}