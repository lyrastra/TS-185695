using System;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

public class EventBusEventDefinition<T>
    where T : class
{
    public EventBusEventDefinition(string exchangeName)
    {
        MessageType = typeof(T);
        ExchangeName = exchangeName;
    }

    public Type MessageType { get; }

    public string ExchangeName { get; }
}