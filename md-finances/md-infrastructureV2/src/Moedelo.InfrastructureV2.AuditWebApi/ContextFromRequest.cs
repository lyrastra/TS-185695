using System;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.AuditWebApi;

public class ContextFromRequest : IAuditSpanContext
{
    public Guid AsyncTraceId { get; set; }

    public Guid TraceId { get; set; }

    public Guid? ParentId { get; set; }

    public Guid CurrentId { get; set; }

    public short CurrentDepth { get; set; }
}