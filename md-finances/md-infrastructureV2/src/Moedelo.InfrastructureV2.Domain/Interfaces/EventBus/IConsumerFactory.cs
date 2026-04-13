using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Models.EventBus;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

public interface IConsumerFactory
{
    Task StartConsumerWithRetryAsync<TMessage>(
        EventBusEventDefinition<TMessage> definition,
        Func<TMessage, Task> onMessage,
        ConsumerWithRetryOptions options = null)
        where TMessage : class;

    delegate Task MessageHandler<in TMessage>(TMessage message, uint repeatCount) where TMessage : class;

    Task StartConsumerWithRetryAsync<TMessage>(
        EventBusEventDefinition<TMessage> definition,
        MessageHandler<TMessage> onMessage,
        ConsumerWithRetryOptions options = null)
        where TMessage : class;
}