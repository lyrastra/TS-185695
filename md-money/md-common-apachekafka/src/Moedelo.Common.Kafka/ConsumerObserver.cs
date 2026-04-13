using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka;

internal class ConsumerObserver<TMessage> : IDisposable where TMessage : KafkaMessageValueBase
{
    private readonly Task consumerTask;
    private readonly ManualResetEventSlim isIdle = new (true);
    private CancellationTokenSource cancellation;

    public ConsumerObserver(
        Func<IKafkaConsumerHandlers<TMessage>, CancellationToken, Task> consumerTaskFactory,
        Func<TMessage, Task> onMessage,
        Func<Exception, Task> onException,
        CancellationToken cancellationToken)
    {
        this.cancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        Task WrapOnMessage(TMessage message, CancellationToken localToken)
        {
            isIdle.Reset();

            return onMessage(message);
        }

        Task WrapOnException(Exception exception)
        {
            SetIdleState();
            return onException?.Invoke(exception) ?? Task.CompletedTask;
        }

        void OnMessageCommitted(TMessage _)
        {
            SetIdleState();
        }

        void OnMessageHandlingFailed(TMessage _, Exception __)
        {
            SetIdleState();
        }

        this.consumerTask = consumerTaskFactory.Invoke(
            new KafkaConsumerHandlers<TMessage>(WrapOnMessage)
                .WithFatalExceptionHandler(WrapOnException)
                .WithMessageCommittedHandler(OnMessageCommitted)
                .WithMessageHandlingFailedHandler(OnMessageHandlingFailed),
            cancellation.Token);
    }

    internal bool IsIdle => isIdle.IsSet;

    private void SetIdleState()
    {
        if (!isIdle.IsSet)
        {
            isIdle.Set();
        }
    }

    public void Dispose()
    {
        try
        {
            CancelConsumer(TimeSpan.FromMinutes(1));
            SetIdleState();
            isIdle.Dispose();
            cancellation?.Dispose();
        }
        catch
        {
            // nothing
        }

        cancellation = null;
    }

    private void CancelConsumer(TimeSpan timeout)
    {
        if (cancellation?.IsCancellationRequested == false)
        {
            cancellation.Cancel();
            try
            {
                consumerTask.Wait(timeout);
            }
            catch
            {
                /* игнорируем ошибки */
            }
        }
    }

    /// <summary>
    /// Подождать, пока закончится обработка сообщения (если оно сейчас обрабатывается)
    /// Предотвратить обработку следующего сообщения
    /// </summary>
    /// <param name="maxSmoothTimeout">максимальное время, которое можно ждать до завершения обработки сообщения</param>
    public async Task<bool> WaitForMessageHandlingCompleteAndStopAsync(TimeSpan maxSmoothTimeout)
    {
        try
        {
            if (!isIdle.IsSet)
            {
                // даже если isIdle изменит состояние между этими строчками в true, всё отработает корректно
                await Task.Run(() => isIdle.Wait(maxSmoothTimeout, cancellation.Token))
                    .ConfigureAwait(false);
            }

            // удалось дождаться завершения обработки сообщения
            return isIdle.IsSet;
        }
        catch (OperationCanceledException)
        {
            /* игнорируем ошибки отмены операции */
        }
        finally
        {
            if (!cancellation.IsCancellationRequested)
            {
                cancellation.Cancel();
            }
        }

        // не удалось дождаться завершения обработки сообщения
        return false;
    }
}