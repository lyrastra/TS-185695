using System;

namespace Moedelo.Common.Kafka.Saga.Abstractions
{
    public sealed class MoedeloSagaInitStateData
    {
        public Guid SagaId { get; set; }

        public string StateType { get; set; }

        public string StateData { get; set; }

        public string ExecutionContextToken { get; set; }
    }
}
