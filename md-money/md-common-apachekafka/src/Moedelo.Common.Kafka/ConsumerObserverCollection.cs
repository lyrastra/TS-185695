using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Extensions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka;

internal sealed class ConsumerObserverCollection<TMessage> : IConsumerObserverCollection, IDisposable
    where TMessage : KafkaMessageValueBase
{
    private readonly ConcurrentQueue<ConsumerObserver<TMessage>> consumers;
    private readonly Func<KafkaConsumerSettings<TMessage>, CancellationToken, Task> consumerTaskFactory;
    private readonly CancellationToken cancellationToken;
    private readonly CancellationTokenRegistration autoDisposeRegistration;
    private readonly KafkaConsumerSettings<TMessage> consumerSettings;
    // ReSharper disable once RedundantDefaultMemberInitializer
    private bool disposed = false;

    public ConsumerObserverCollection(KafkaConsumerSettings<TMessage> consumerSettings,
        Func<KafkaConsumerSettings<TMessage>, CancellationToken, Task> consumerTaskFactory,
        CancellationToken cancellationToken)
    {
        consumers = new ConcurrentQueue<ConsumerObserver<TMessage>>();
        this.consumerSettings = consumerSettings;
        this.consumerTaskFactory = consumerTaskFactory;
        this.cancellationToken = cancellationToken;

        autoDisposeRegistration = cancellationToken.Register(Dispose);
    }

    /// <summary>
    ///  Количество консьюмеров
    /// </summary>
    public int Count => consumers.Count;

    public KafkaConsumerConfig Settings => consumerSettings.Config;

    public void Dispose()
    {
        if (disposed)
            return;
        disposed = true;

        autoDisposeRegistration.Dispose();
        consumers.SafeClear();
    }

    public void StartNew()
    {
        consumers.Enqueue(new ConsumerObserver<TMessage>(
            CallConsumerTaskFactory,
            m => consumerSettings.Handlers.HandleMessage(m, CancellationToken.None),
            consumerSettings.Handlers.OnFatalException,
            cancellationToken));
    }

    private Task CallConsumerTaskFactory(
        IKafkaConsumerHandlers<TMessage> handlers,
        CancellationToken ct)
    {
        var fixedConsumerSettings = consumerSettings with { Handlers = handlers };

        return consumerTaskFactory(fixedConsumerSettings, ct);
    }

    public async Task<bool> StopAndRemoveConsumerAsync(TimeSpan maxStopTimeout)
    {
        if (consumers.TryDequeue(out var removed))
        {
            await removed
                .WaitForMessageHandlingCompleteAndStopAsync(maxStopTimeout)
                .ConfigureAwait(false);
            removed.Dispose();
            return true;
        }

        return false;
    }
}
