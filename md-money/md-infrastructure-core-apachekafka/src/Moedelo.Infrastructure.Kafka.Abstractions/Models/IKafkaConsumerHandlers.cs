using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models;

public interface IKafkaConsumerHandlers<in TMessage> where TMessage : KafkaMessageValueBase
{
    /// <summary>
    /// Обработчик сообщения. Вызывается после того, как сообщение прочитано из топика
    /// </summary>
    Task HandleMessage(TMessage message, CancellationToken token);

    /// <summary>
    /// Обработчик события "Обработка сообщения прервана ошибкой"
    /// </summary>
    /// <param name="message">сообщение, которое было передано на обработку</param>
    /// <param name="exception">произошедшее исключение</param>
    void OnMessageHandlingFailed(TMessage message, Exception exception);
    
    /// <summary>
    /// Обработчик события "Сообщение закоммичено в топик как обработанное"
    /// </summary>
    void OnMessageCommitted(TMessage message);

    /// <summary>
    /// Обработчик события "Обработка собыий приостановлена из-за фатального сбоя"
    /// </summary>
    Task OnFatalException(Exception exception);
}