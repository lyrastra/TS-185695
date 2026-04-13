namespace Moedelo.Common.Kafka.Abstractions.Base
{
    public abstract class MoedeloKafkaMessageDefinitionBase : IMoedeloKafkaMessageDefinition
    {
        public string TopicName { get; set; }

        public string KeyMessage { get; set; }
    }
}