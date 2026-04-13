using System;

namespace Moedelo.Common.Audit.Abstractions.Interfaces
{
    public interface IAuditScope : IDisposable
    {
        IAuditSpan Span { get; }
        
        bool IsEnabled { get; }
    }
}