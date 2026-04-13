using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models;

public class KafkaConsumerHandlers<TMessage> : IKafkaConsumerHandlers<TMessage> where TMessage : KafkaMessageValueBase
{
    private readonly Func<TMessage, CancellationToken, Task> messageHandler;
    private Func<Exception, Task>? onFatalException;
    private Action<TMessage>? onMessageCommitted;
    private Action<TMessage, Exception>? onMessageHandlingFailed;

    public KafkaConsumerHandlers(Func<TMessage, CancellationToken, Task> messageHandler)
    {
        this.messageHandler = messageHandler ?? throw new ArgumentNullException(nameof(messageHandler));
    }

    public KafkaConsumerHandlers<TMessage> WithFatalExceptionHandler(Func<Exception, Task> handler)
    {
        Debug.Assert(onFatalException == null, "Already set");
        Debug.Assert(handler != null, "Can't be null");

        onFatalException = handler!;

        return this;
    }

    public KafkaConsumerHandlers<TMessage> WithMessageCommittedHandler(
        Action<TMessage> handler)
    {
        Debug.Assert(onMessageCommitted == null, "Already set");
        Debug.Assert(handler != null, "Can't be null");

        onMessageCommitted = handler!;

        return this;
    }

    public KafkaConsumerHandlers<TMessage> WithMessageHandlingFailedHandler(
        Action<TMessage, Exception> handler)
    {
        Debug.Assert(onMessageHandlingFailed == null, "Already set");
        Debug.Assert(handler != null, "Can't be null");

        onMessageHandlingFailed = handler!;

        return this;
    }

    public Task HandleMessage(TMessage message, CancellationToken token)
    {
        return messageHandler.Invoke(message, token);
    }

    public void OnMessageHandlingFailed(TMessage message, Exception exception)
    {
        onMessageHandlingFailed?.Invoke(message, exception);
    }

    public Task OnFatalException(Exception exception)
    {
        return onFatalException?.Invoke(exception) ?? Task.CompletedTask;
    }

    public void OnMessageCommitted(TMessage message)
    {
        onMessageCommitted?.Invoke(message);
    }
}
