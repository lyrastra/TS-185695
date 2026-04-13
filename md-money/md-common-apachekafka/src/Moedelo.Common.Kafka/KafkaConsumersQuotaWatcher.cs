using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Consul.Abstractions;
using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka;

[InjectAsSingleton(typeof(IKafkaConsumersQuotaWatcher))]
internal sealed class KafkaConsumersQuotaWatcher : IKafkaConsumersQuotaWatcher
{
    private readonly ILogger<KafkaConsumersQuotaWatcher> logger;
    private readonly IMoedeloConsulCatalogWatcher consulCatalogWatcher;
    private readonly IKafkaConsumersQuotaWatchingSettings watchingSettings;
    private readonly ConcurrentDictionary<string, Lazy<CancellationTokenSource>> watchingTasks = new();
    private readonly ConcurrentDictionary<string, ServiceKafkaBalanceStateWrap> values = new();

    public KafkaConsumersQuotaWatcher(
        IMoedeloConsulCatalogWatcher consulCatalogWatcher,
        ILogger<KafkaConsumersQuotaWatcher> logger,
        IKafkaConsumersQuotaWatchingSettings watchingSettings)
    {
        this.consulCatalogWatcher = consulCatalogWatcher;
        this.logger = logger;
        this.watchingSettings = watchingSettings;
    }

    private string ConsulRootDirectory => watchingSettings.ConsulTopicsKeyValueRootDirectory;

    public ServiceKafkaBalanceContext CreateServiceKafkaBalanceContext(
        KafkaConsumerConfig kafkaConsumerSettings,
        string serviceId)
    {
        var topic = kafkaConsumerSettings.TopicName.Raw;
        var consumerGroupId = kafkaConsumerSettings.GroupId.Raw;
        var keyPath = $"{ConsulRootDirectory}/{topic}/{consumerGroupId}/{serviceId}";

        return new ServiceKafkaBalanceContext(serviceId, kafkaConsumerSettings, keyPath);
    }

    public Task WatchQuotaChangesAsync(
        ServiceKafkaBalanceContext context,
        Func<ServiceKafkaBalanceState, Task> onQuotaChanged,
        CancellationToken cancellationToken)
    {
        var rootPath = ConsulRootDirectory;

        var relKeyPath = context.ConsulKeyPath.Substring(rootPath.Length + 1); 
        var state = values.AddOrUpdate(
            relKeyPath,
            _ => new ServiceKafkaBalanceStateWrap(),
            (_, state) => state);

        Task HandlerWrapper(ServiceKafkaBalanceState s) => onQuotaChanged(s);
        state.OnChange += HandlerWrapper;

        var lazyToken = watchingTasks.GetOrAdd(rootPath, path => new Lazy<CancellationTokenSource>(() =>
        {
            var cancellation = new CancellationTokenSource();
            consulCatalogWatcher.WatchDirectory(
                path,
                (keyValues, token) => OnSomeChangesInRootDirectory(context, keyValues, token),
                cancellation.Token);

            return cancellation;
        }));

        var subscriptionCancellation = lazyToken.Value;
        cancellationToken.Register(() =>
        {
            state.OnChange -= HandlerWrapper;

            try
            {
                if (!subscriptionCancellation.IsCancellationRequested)
                    subscriptionCancellation.Cancel(false);
            }
            catch
            {
                /* ignoring exceptions */
            }
        });

        return Task.CompletedTask;
    }

    private async Task OnSomeChangesInRootDirectory(
        ServiceKafkaBalanceContext balanceContext,
        IReadOnlyCollection<KeyValuePair<string, string>> keyValues,
        CancellationToken cancellationToken)
    {
        var serviceId = balanceContext.ServiceId;
        var myServiceQuotaKeys = keyValues
            .Where(kv => kv.Key.EndsWith(serviceId))
            .ToArray();

        foreach (var kv in myServiceQuotaKeys)
        {
            var key = kv.Key.Replace(':', '/');
            try
            {
                var stateWrap = values.GetOrAdd(key, _ => new ServiceKafkaBalanceStateWrap(kv.Value));
                if (kv.Value != stateWrap.JsonString)
                {
                    await stateWrap.SetJsonStringAsync(kv.Value);
                }
                cancellationToken.ThrowIfCancellationRequested();
            }
            catch (Exception exception) when (!cancellationToken.IsCancellationRequested)
            {
                logger.LogError(exception, $"Ошибка при попытке обновления значения ключа {key}");
            }
        }
        // отдельный кейс: локально есть подписка на ключ, которого больше нет в consul
        var myServiceKeys = new HashSet<string>(
            myServiceQuotaKeys.Select(kv => kv.Key.Replace(':', '/')));
        var missedList = values
            .Where(kv => !myServiceKeys.Contains(kv.Key));
        foreach(var keyValue in missedList)
        {
            var subscription = keyValue.Value;
            if (!string.IsNullOrEmpty(subscription.JsonString))
            {
                logger.LogWarning($"Обнаружена пропажа ключа балансировки {keyValue.Key}. Будет выполнена попытка восстановить");
            }

            await subscription.NotifyAboutQuotaIsMissedAsync();
        }
    }
}
