using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Extensions;
using Moedelo.Infrastructure.Kafka.Internals;

namespace Moedelo.Infrastructure.Kafka;

[InjectAsSingleton(typeof(IKafkaConsumerStarter))]
internal sealed class KafkaConsumerStarter : IKafkaConsumerStarter, IAsyncDisposable
{
    private readonly IDefaultKafkaConsumerFactory defaultConsumerFactory;
    private readonly IKafkaConsumerFactory[] kafkaConsumerFactories;
    private readonly IKafkaConsumerFactorySettings settings;
    private readonly IKafkaConsumeExecutor consumeExecutor;
    private readonly ILogger logger;

    /// <summary>
    /// Источник отмены всех запущенных тасков консьюмеров
    /// </summary>
    private readonly CancellationTokenSource consumersCancellation = new ();

    /// <summary>
    /// Коллекция запущенных тасков консьюмеров
    /// </summary>
    private readonly ConcurrentQueue<Task> consumeTasks = new ();

    private bool disposed;

    public KafkaConsumerStarter(
        IKafkaConsumerFactorySettings settings,
        IEnumerable<IKafkaConsumerFactory> kafkaConsumerFactories,
        ILogger<KafkaConsumerStarter> logger,
        IDefaultKafkaConsumerFactory defaultConsumerFactory,
        IKafkaConsumeExecutor consumeExecutor)
    {
        this.logger = logger;
        this.settings = settings;
        this.defaultConsumerFactory = defaultConsumerFactory;
        this.kafkaConsumerFactories = kafkaConsumerFactories.ToArray();
        this.consumeExecutor = consumeExecutor;
    }

    public event IKafkaConsumerStarter.ConsumerStarted? ConsumerStartedEvent
    {
        add => consumeExecutor.EventSource.ConsumerStartedEvent += value;
        remove => consumeExecutor.EventSource.ConsumerStartedEvent -= value;
    }
    public event IKafkaConsumerStarter.ConsumerStopped? ConsumerStoppedEvent
    {
        add => consumeExecutor.EventSource.ConsumerStoppedEvent += value;
        remove => consumeExecutor.EventSource.ConsumerStoppedEvent -= value;
    }
    public event IKafkaConsumerStarter.PartitionSetOnPause? PartitionSetOnPauseEvent
    {
        add => consumeExecutor.EventSource.PartitionSetOnPauseEvent += value;
        remove => consumeExecutor.EventSource.PartitionSetOnPauseEvent -= value;
    }

    public Task ListenAsync<TMessage>(
        KafkaConsumerSettings<TMessage> consumerSettings,
        CancellationToken cancellationToken) where TMessage : KafkaMessageValueBase
    {
        CheckDisposed();

        var consumerFactory = ResolveConsumerFactory(consumerSettings.ConsumerFactoryType);

        var task = StartConsumeTask(
            consumerFactory,
            consumerSettings.Config.EnsureIsNotNull(nameof(consumerSettings.Config)),
            consumerSettings.Handlers.EnsureIsNotNull(nameof(consumerSettings.Handlers)),
            cancellationToken);

        consumeTasks.Enqueue(task);

        return task;
    }

    private IKafkaConsumerFactory ResolveConsumerFactory(Type? requiredType)
    {
        if (requiredType == null)
        {
            return defaultConsumerFactory;
        }

        return kafkaConsumerFactories
            .SingleOrDefault(requiredType.IsInstanceOfType)
            .EnsureIsNotNull("memoryFactory", $"Не удалось найти экземпляр фабрики типа {requiredType.Name}. Проверьте конфигурацию: подключена ли сборка {requiredType.Assembly.FullName} к проекту приложения");
    }

    private async Task StartConsumeTask<TMessage>(
        IKafkaConsumerFactory consumerFactory,
        KafkaConsumerConfig config,
        IKafkaConsumerHandlers<TMessage> handlers,
        CancellationToken cancellationToken) where TMessage : KafkaMessageValueBase
    {
        // отпускаем текущий таск, чтобы он не блокировал текущий поток (на старте приложения может запускаться много консьюмеров)
        await Task.Yield();

        using var consumerCancellation = CancellationTokenSource
            .CreateLinkedTokenSource(cancellationToken, consumersCancellation.Token);

        try
        {
            using var kafkaConsumer = (await consumerFactory
                .CreateAsync(config, settings, logger)
                .ConfigureAwait(false))
                .EnsureIsNotNull("kafkaConsumer", "Фабрика консьюмеров вернула null");

            ConsumerGroupIdStats.RegisterGroupId(config.GroupId);

            // запускаем "бесконечный" цикл чтения сообщений из топика
            await consumeExecutor
                .RunConsumeLoopAsync(config, handlers, kafkaConsumer, consumerCancellation.Token)
                .ConfigureAwait(false);
        }
        catch (Exception exception) when (consumerCancellation.IsCancellationRequested == false)
        {
            if (exception is KafkaException kafkaException)
            {
                // это особый случай, лучше его залогировать на этом уровне отдельно
                logger.LogKafkaException(config, kafkaException, config.GroupId);
            }

            // обработку и логирование всех ошибок делегируем заявленному обработчику
            await handlers.OnFatalException(exception).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (consumerCancellation.IsCancellationRequested)
        {
            // работа консьюмера была отменена штатным образом => просто заканчиваем выполнение
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
    }

    private async ValueTask DisposeAsync(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        if (disposing)
        {
            if (!consumersCancellation.IsCancellationRequested)
            {
                consumersCancellation.Cancel();
                await consumeTasks.SafeClearAndWaitAsync(TimeSpan.FromMinutes(1));
            }
            consumersCancellation.Dispose();

            disposed = true;
        }
    }

    private void CheckDisposed()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(KafkaConsumerStarter));
        }
    }
}
