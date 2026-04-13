using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.AccountingStatements.Kafka.Abstractions
{
    public class CDEventMessageValue : MoedeloKafkaMessageValueBase
    {
        public AccountingStatementEventType EventType { get; set; }

        public string EventDataJson { get; set; }
    }
    
    public enum AccountingStatementEventType
    {
        Created = 1,
        Deleted = 3,
    }
}