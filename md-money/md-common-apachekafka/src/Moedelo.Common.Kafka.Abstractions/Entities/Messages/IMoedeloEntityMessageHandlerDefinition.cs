#nullable enable
using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Messages;

public interface IMoedeloEntityMessageHandlerDefinition<out TDefinition, out TMessageValue>
    where TMessageValue: MoedeloKafkaMessageValueBase
{
    /// <summary>
    /// Не извлекать информацию о контексте исполнения (ExecutionContext) из сообщений.
    /// По умолчанию из сообщения извлекается информация о контексте исполнения, создаётся соответствующий контекст исполнения
    /// и обработчик сообщения вызывается уже при выставленном данном контексте исполнения.
    /// ВНИМАНИЕ: если необходимо отключить извлечение и создание контекста исполнения, это надо сделать в двух местах:
    /// - на уровне настройки чтения из топика <see cref="MoedeloEntityMessageKafkaTopicReaderBuilder.WithoutExecutionContext"/>
    /// - на уровне настройки чтения конкретного типа сообщения (используя данный метод)
    /// </summary>
    TDefinition WithoutContext();

    /// <summary>
    /// Зарегистрировать колбэк, который будет вызван при ошибке десериализации токена контекста исполнения 
    /// </summary>
    /// <param name="onTokenDeserializationFailed">колбэк</param>
    TDefinition SetExecutionContextTokenFallback(
        Func<TMessageValue, Exception, ValueTask<string>>? onTokenDeserializationFailed);

    TDefinition SetWithRetry(bool withRetry, ConsumerActionRetrySettings settings);

    TDefinition SetDebugLogging(bool value);

    TDefinition SetOnMessageExceptionHandler(Func<TMessageValue, Exception, Task>? onException);

    /// <summary>
    /// Игнорировать то, что контекст исполнения устарел.
    /// ВНИМАНИЕ: надо очень хорошо понимать, какие действия в таком контексте можно выполнять, а какие нет
    /// </summary>
    /// <returns></returns>
    TDefinition IgnoreExecutionContextOutdating(bool value);
}
