using System;
using System.Collections.Generic;
using Moedelo.Common.Audit.Abstractions.Models;

namespace Moedelo.Common.Audit.Abstractions.Interfaces;

public interface IAuditSpanData
{
    IAuditSpanContext Context { get; }
        
    AuditSpanType Type { get; }
        
    /// <summary>
    /// Хост. Если не заполнено - будет заполнено автоматически значением имени текущего компьютера
    /// </summary>
    string Host { get; }
        
    string AppName { get; }

    string Name { get; }

    DateTime StartDateUtc { get; }

    DateTime FinishDateUtc { get; }

    bool HasError { get; }
        
    IReadOnlyDictionary<string, List<object>> Tags { get; }
}
