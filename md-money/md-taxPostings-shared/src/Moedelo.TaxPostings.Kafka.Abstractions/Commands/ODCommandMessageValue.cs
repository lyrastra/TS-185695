using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Commands
{
    public class ODCommandMessageValue : MoedeloKafkaMessageValueBase
    {
        public PostingsCommandType CommandType { get; set; }

        public string CommandDataJson { get; set; }
    }
}
