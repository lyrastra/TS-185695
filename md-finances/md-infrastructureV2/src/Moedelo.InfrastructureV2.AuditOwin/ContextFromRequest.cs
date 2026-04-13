using System;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.AuditOwin;

/// <summary>
/// Модель для десериализации родительского контекста аудита из заголовков запроса
/// </summary>
public class ContextFromRequest : IAuditSpanContext
{
    /// <summary>
    /// Идентификатор асинхронного трейса
    /// </summary>
    public Guid AsyncTraceId { get; set; }

    /// <summary>
    /// Идентификатор трейса
    /// </summary>
    public Guid TraceId { get; set; }

    /// <summary>
    /// Идентификатор родительского спана
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// Идентификатор текущего спана
    /// </summary>
    public Guid CurrentId { get; set; }

    /// <summary>
    /// Текущая глубина вложенности
    /// </summary>
    public short CurrentDepth { get; set; }
}
