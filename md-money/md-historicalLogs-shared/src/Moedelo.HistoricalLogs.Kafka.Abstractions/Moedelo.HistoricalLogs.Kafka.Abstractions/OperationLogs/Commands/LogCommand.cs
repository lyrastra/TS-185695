using System;
using Moedelo.HistoricalLogs.Enums;

namespace Moedelo.HistoricalLogs.Kafka.Abstractions.OperationLogs.Commands;

[Obsolete]
public class LogCommand
{
    public LogOperationType OperationType { get; set; }

    public LogObjectType ObjectType { get; set; }

    public long ObjectId { get; set; }

    public string ObjectData { get; set; }
}