namespace Moedelo.InfrastructureV2.Domain.AuditModule.Models;

public enum AuditSpanType : byte
{
    /// <summary>
    /// Входящее обращение по http api
    /// </summary>
    ApiHandler = 1,

    /// <summary>
    /// Исходящий вызов http api
    /// </summary>
    OutgoingHttpRequest = 2,

    /// <summary>
    /// Исходящий вызов к БД
    /// (не используется, зарезервировано на всякий случай)
    /// </summary>
    DbQuery = 3,

    /// <summary>
    /// Обработка событий шины событий (rabbitmq, kafka)
    /// </summary>
    EventApiHandler = 5,

    /// <summary>
    /// Исходящий вызов к БД mssql
    /// </summary>
    MsSqlDbQuery = 10,

    /// <summary>
    /// Исходящий вызов к БД postgresql
    /// </summary>
    PostgreSQLDbQuery = 11,

    /// <summary>
    /// Исходящий вызов к БД mysql
    /// </summary>
    MySqlDbQuery = 12,

    /// <summary>
    /// Исходящий вызов к БД mongo
    /// </summary>
    MongoDbQuery = 13,

    /// <summary>
    /// Исходящий запрос к БД redis
    /// </summary>
    RedisDbQuery = 14,

    /// <summary>
    /// Исходящий запрос к БД clickhouse
    /// </summary>
    ClickhouseDbQuery = 15,

    CloudProcesses = 16,

    /// <summary>
    /// "Распределенённый захват ресурса с использованием ключа Redis"
    /// Время спана = время от начала захвата блокировки до её освобождения
    /// Внутри спана есть несколько обращений к Redis, они не отражаются отдельными спанами типа RedisDbQuery
    /// </summary>
    RedisDistributedLock = 94,

    /// <summary>
    /// Любой код, не подпадающий под другие определённые типы
    /// </summary>
    InternalCode = 99
}
