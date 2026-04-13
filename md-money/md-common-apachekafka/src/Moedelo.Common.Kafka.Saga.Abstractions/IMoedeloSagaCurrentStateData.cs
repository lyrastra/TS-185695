using System;

namespace Moedelo.Common.Kafka.Saga.Abstractions
{
    public interface IMoedeloSagaCurrentStateData
    {
        public Guid SagaId { get; }
        
        public string StateType { get; }
        
        public string StateData { get; }
        
        public string ExecutionContextToken { get; }
    }
}
