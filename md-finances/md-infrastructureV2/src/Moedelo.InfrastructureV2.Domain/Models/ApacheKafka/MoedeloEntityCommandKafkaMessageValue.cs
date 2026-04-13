using System;

namespace Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

public sealed class MoedeloEntityCommandKafkaMessageValue<T>  : MoedeloKafkaMessageValueBase
{
    public MoedeloEntityCommandKafkaMessageValue(string entityType, string commandType, T commandData)
    {
        if (string.IsNullOrWhiteSpace(entityType))
        {
            throw new ArgumentNullException(nameof(entityType));
        }
            
        if (string.IsNullOrWhiteSpace(commandType))
        {
            throw new ArgumentNullException(nameof(commandType));
        }

        if (commandType == null)
        {
            throw new ArgumentNullException(nameof(commandType));
        }
            
        EntityType = entityType;
        CommandType = commandType;
        CommandData = commandData;
    }

    public string EntityType { get; }
        
    public string CommandType { get; }

    public T CommandData { get; }
}