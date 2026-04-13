using System;

namespace Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

public interface IAuditScope : IDisposable
{
    IAuditSpan Span { get; }

    bool IsEnabled { get; }
}