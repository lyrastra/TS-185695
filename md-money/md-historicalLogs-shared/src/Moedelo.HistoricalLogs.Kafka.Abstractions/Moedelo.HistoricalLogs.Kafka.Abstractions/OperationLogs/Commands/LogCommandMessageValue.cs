using System;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.HistoricalLogs.Enums;

namespace Moedelo.HistoricalLogs.Kafka.Abstractions.OperationLogs.Commands;

[Obsolete]
public class LogCommandMessageValue : MoedeloKafkaMessageValueBase
{
    public LogOperationType OperationType { get; set; }

    public LogObjectType ObjectType { get; set; }

    public long ObjectId { get; set; }

    public string ObjectData { get; set; }
}