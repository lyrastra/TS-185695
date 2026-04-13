using System;

namespace Moedelo.Common.Audit.Abstractions.Interfaces;

public interface IAuditSpan : IAuditSpanData
{
    void AddTag(string tagName, object tagValue);
        
    void SetError();

    void SetError(Exception exception);

    void Finish();
        
    void Finish(DateTime finishDateUtc);

    void SetName(string value);
}