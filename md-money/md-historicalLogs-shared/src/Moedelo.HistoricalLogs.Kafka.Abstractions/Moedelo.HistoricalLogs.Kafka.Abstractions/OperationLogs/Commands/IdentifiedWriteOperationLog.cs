using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.HistoricalLogs.Enums;

namespace Moedelo.HistoricalLogs.Kafka.Abstractions.OperationLogs.Commands;

/// <summary>
/// Запись в операционный журнал, включающая FirmId и UserId
/// При логировании такой записи контекст исполнения (ExecutionContext) игнорируется
/// </summary>
public class IdentifiedWriteOperationLog : IEntityCommandData
{
    public int FirmId { get; set; }

    public int UserId { get; set; }

    public LogOperationType OperationType { get; set; }

    public LogObjectType ObjectType { get; set; }

    public long ObjectId { get; set; }

    public string ObjectData { get; set; }

    public DateTime CreateDate { get; set; }
}