using System;

namespace Moedelo.Common.Kafka.Saga.Abstractions
{
    public sealed class MoedeloSagaNewStateData
    {
        public Guid SagaId { get; set; }
        
        public string StateType { get; set; }
        
        public string StateData { get; set; }
    }
}
