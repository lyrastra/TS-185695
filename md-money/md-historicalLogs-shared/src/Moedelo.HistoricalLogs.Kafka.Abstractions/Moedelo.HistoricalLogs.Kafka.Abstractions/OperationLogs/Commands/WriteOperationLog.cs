using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.HistoricalLogs.Enums;

namespace Moedelo.HistoricalLogs.Kafka.Abstractions.OperationLogs.Commands;

public class WriteOperationLog : IEntityCommandData
{
    public LogOperationType OperationType { get; set; }

    public LogObjectType ObjectType { get; set; }

    public long ObjectId { get; set; }

    public string ObjectData { get; set; }

    public DateTime? CreateDate { get; set; } = DateTime.Now;
}