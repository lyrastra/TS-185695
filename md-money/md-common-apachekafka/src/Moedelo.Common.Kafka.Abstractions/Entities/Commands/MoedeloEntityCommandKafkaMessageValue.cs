using System;
using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands
{
    public sealed class MoedeloEntityCommandKafkaMessageValue : MoedeloKafkaMessageValueBase
    {
        public MoedeloEntityCommandKafkaMessageValue(string entityType, string commandType, object commandData, string transmittedRef = null)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }
            
            if (string.IsNullOrWhiteSpace(commandType))
            {
                throw new ArgumentNullException(nameof(commandType));
            }

            if (transmittedRef == null && commandData == null)
            {
                throw new ArgumentNullException(nameof(commandData));
            }
            
            EntityType = entityType;
            CommandType = commandType;
            CommandData = transmittedRef == null ? commandData : null;
            TransmittedRef = transmittedRef;
        }

        public string EntityType { get; }
        
        public string CommandType { get; }

        public object CommandData { get; set; }
    }
}
