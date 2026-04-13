using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Configuration.Helpers;
using Moedelo.Common.Audit.Configuration.Interfaces;
using Moedelo.Common.Audit.Configuration.Models;
using Moedelo.Common.Audit.Extensions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Audit.Configuration
{
    [InjectAsSingleton(typeof(ICommandStateManager))]
    internal sealed class CommandStateManager : ICommandStateManager, IDisposable
    {
        private static readonly TimeSpan RefreshDelay = TimeSpan.FromSeconds(5);
        private readonly CancellationTokenSource cts = new ();
        private bool disposed;
        private readonly Task loopTask;
        private readonly IAuditRedisDbExecuter auditRedisDbExecutor;

        public CommandStateManager(
            IAuditRedisDbExecuter auditRedisDbExecutor,
            ISettingsConfigurations settingsConfigurations,
            ISettingRepository settingRepository)
        {
            Current = new CommandState();

            this.auditRedisDbExecutor = auditRedisDbExecutor;
            var isEnabled = settingRepository.IsAuditTrailEnabled();
            AuditTrailAppName = GetAuditTrailAppName(settingsConfigurations.Config, settingRepository);

            if (isEnabled && string.IsNullOrWhiteSpace(AuditTrailAppName) == false)
            {
                loopTask = LoopAsync(cts.Token);
            }
            else
            {
                loopTask = Task.CompletedTask;
            }
        }

        private static string GetAuditTrailAppName(
            SettingsConfig appConfig, ISettingRepository settingRepository)
        {
            var appName = appConfig.AuditTrailAppName;

            return string.IsNullOrEmpty(appName) == false
                ? appName
                : settingRepository.GetAuditAppName().Value;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (cts != null)
                {
                    cts.Cancel();
                    try
                    {
                        loopTask.Wait(TimeSpan.FromSeconds(1));
                    }
                    catch
                    {
                        // nothing to do
                    }

                    cts.Dispose();
                }
            }

            disposed = true;
        }

        ~CommandStateManager()
        {
            Dispose(false);
        }

        public string AuditTrailAppName { get; }

        public CommandState Current { get; private set; }

        private Task LoopAsync(CancellationToken cancellationToken)
        {
            var appIsRunningMakerKey = KeysHelper.GetCommandKey(AuditTrailAppName, "run");
            const string appIsRunningMakerKeyValue = "run";
            var redisKeys = new []
            {
                KeysHelper.GetCommandKey(AuditTrailAppName, CommandStateBuilder.CommandEnable),
                KeysHelper.GetCommandKey(AuditTrailAppName, CommandStateBuilder.CommandApiHandler),
                KeysHelper.GetCommandKey(AuditTrailAppName, CommandStateBuilder.CommandOutgoingHttpRequest),
                KeysHelper.GetCommandKey(AuditTrailAppName, CommandStateBuilder.CommandDbQuery),
                KeysHelper.GetCommandKey(AuditTrailAppName, CommandStateBuilder.CommandInternalCode),
            };
            
            return Task.Factory.StartNew(async () =>
            {
                try
                {
                    
                    await auditRedisDbExecutor
                        .SetValueForKeyAsync(appIsRunningMakerKey, appIsRunningMakerKeyValue)
                        .ConfigureAwait(false);
                }
                catch
                {
                    //ignore
                }

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var state = await LoadStateAsync(redisKeys).ConfigureAwait(false);
                        Current = state;
                    }
                    catch
                    {
                        //ignore
                    }

                    try
                    {
                        await Task.Delay(RefreshDelay, cancellationToken).ConfigureAwait(false);
                    }
                    catch (TaskCanceledException)
                    {
                        break;
                    }
                }
            }, TaskCreationOptions.LongRunning).Unwrap();
        }

        private async Task<CommandState> LoadStateAsync(IReadOnlyList<string> redisKeys)
        {
            //читаем команды напрямую по ключам
            var values = await auditRedisDbExecutor.GetValueListByKeyListAsync(redisKeys).ConfigureAwait(false);
            var grouping = values
                .Select((data, index) =>
                {
                    var key = KeysHelper.ParseKey(redisKeys[index]);
                    return new KeyData(key, data);
                })
                .GroupBy(keyData => keyData.Key.AppName);

            return ToCommandState(grouping.FirstOrDefault());
        }

        private static CommandState ToCommandState(IEnumerable<KeyData> groupData)
        {
            var csb = new CommandStateBuilder();

            if (groupData == null)
            {
                return csb.Build();
            }

            foreach (var data in groupData)
            {
                if(bool.TryParse(data.Value, out var val)){
                    csb.SetValue(data.Key.Command, val);
                }
            }

            return csb.Build();
        }
    }
}