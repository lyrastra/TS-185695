using System;
using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;

namespace Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

public interface IAuditSpan
{
    IAuditSpanContext Context { get; }
        
    AuditSpanType Type { get; }
        
    string AppName { get; }

    string Name { get; }
        
    bool IsNameNormalized { get; }

    DateTimeOffset StartDateUtc { get; }

    DateTimeOffset FinishDateUtc { get; }

    bool HasError { get; }
        
    IReadOnlyDictionary<string, List<object>> Tags { get; }

    void AddTag(string tagName, object tagValue);

    void SetError();
        
    void SetError(Exception ex);

    void Finish();

    void Finish(DateTimeOffset finishDateUtc);

    void SetNormalizedName(string name);
}
