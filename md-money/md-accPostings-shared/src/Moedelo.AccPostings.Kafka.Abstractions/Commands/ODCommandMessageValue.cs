using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.AccPostings.Kafka.Abstractions.Commands
{
    public class ODCommandMessageValue : MoedeloKafkaMessageValueBase
    {
        public PostingsCommandType CommandType { get; set; }

        public string CommandDataJson { get; set; }
    }
}
