using System.Collections.Generic;
using Moedelo.HistoricalLogs.Kafka.Abstractions.OperationLogs.Commands;
using System.Threading.Tasks;

namespace Moedelo.HistoricalLogs.Kafka.Abstractions.OperationLogs;

public interface IOperationLogsCommandWriter
{
    /// <summary>
    /// Сделать запись в операционный журнал.
    /// ВНИМАНИЕ: должно выполняться в контексте исполнения (execution context) фирмы и пользователя, для которых делается запись, 
    /// </summary>
    /// <param name="commandData"></param>
    /// <returns></returns>
    Task WriteLogAsync(WriteOperationLog commandData);
    /// <summary>
    /// Сделать запись в операционный журнал.
    /// FirmId и UserId передаются в составе сообщения. Данные из ExecutionContext игнорируются
    /// </summary>
    /// <param name="commandData"></param>
    /// <returns></returns>
    Task WriteLogAsync(IdentifiedWriteOperationLog commandData);
    /// <summary>
    /// Постановка в очередь на запись в операционный журнал пачки сообщений.
    /// FirmId и UserId передаются в составе сообщений. Данные из ExecutionContext игнорируются
    /// Гарантии доставки ослаблены:
    /// нет гарантии, что на момент возврата из метода запись в кафку уже будет воспроизведена.
    /// На практике очередь будет оправлена в течение следующих нескольких секунд,
    /// однако сбой в работе приложения или чрезмерная нагрузка могут привести к отмене отправки  
    /// </summary>
    /// <param name="commandDataList">список записей</param>
    /// <returns></returns>
    Task WriteLogAsync(IReadOnlyCollection<IdentifiedWriteOperationLog> commandDataList);
}