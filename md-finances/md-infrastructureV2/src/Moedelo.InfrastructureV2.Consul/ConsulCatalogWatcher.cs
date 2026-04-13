using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using IConsulCatalogWatcher = Moedelo.InfrastructureV2.Domain.Interfaces.Consul.IConsulCatalogWatcher;
using ICoreConsulCatalogWatcher = Moedelo.Infrastructure.Consul.Abstraction.IConsulCatalogWatcher;

namespace Moedelo.InfrastructureV2.Consul
{
    [InjectAsSingleton(typeof(IConsulCatalogWatcher))]
    internal sealed class ConsulCatalogWatcher : IConsulCatalogWatcher, IDisposable
    {
        private readonly ICoreConsulCatalogWatcher catalogWatcher;

        private const string Tag = nameof(ConsulCatalogWatcher);
        private const string ConsulPath = "/";
        private readonly ILogger logger;

        public ConsulCatalogWatcher(
            ILogger logger,
            ICoreConsulCatalogWatcher catalogWatcher)
        {
            this.logger = logger;
            this.catalogWatcher = catalogWatcher;

            catalogWatcher.OnError += OnError;
            catalogWatcher.OnRestoreAfterError += OnRestoreAfterError;
        }

        private void OnRestoreAfterError(int runningErrorsCount, string keyPath)
        {
            logger.Info(Tag, $"Загрузка данных с '{keyPath}' восстановлена",
                extraData: new {runningErrorsCount});
        }

        private void OnError(int runningErrorsCount, string keyPath, string lastErrorMessage)
        {
            var needToLog = (1 < runningErrorsCount && runningErrorsCount <= 10) || runningErrorsCount % 10 == 0;

            if (needToLog)
            {
                logger.Error(Tag, $"Ошибка загрузки данных с '{keyPath}'. Неудачных попыток подряд: {runningErrorsCount}. Последняя ошибка: {lastErrorMessage}");
            }
        }

        public void Dispose()
        {
            catalogWatcher.OnError -= OnError;
            catalogWatcher.OnRestoreAfterError -= OnRestoreAfterError;
        }

        public Task AddWatchCatalogAsync(string keyPath, Action<IReadOnlyCollection<KeyValuePair<string, string>>> onChange, int delayRetrySec = 120)
        {
            return catalogWatcher.LoadAndWatchDirectoryAsync(keyPath, onChange, CancellationToken.None);
        }

        public Task AddStaticCatalogAsync(string keyPath, Action<IReadOnlyCollection<KeyValuePair<string, string>>> onChange)
        {
            keyPath = FormValidKey(keyPath);

            return catalogWatcher.LoadAndWatchDirectoryAsync(keyPath, onChange, CancellationToken.None);
        }

        private static string FormValidKey(string keyPath)
        {
            if (keyPath.EndsWith(ConsulPath) == false)
            {
                keyPath += ConsulPath;
            }

            if (keyPath.StartsWith(ConsulPath))
            {
                keyPath = keyPath.TrimStart(ConsulPath[0]);
            }

            return keyPath;
        }
    }
}