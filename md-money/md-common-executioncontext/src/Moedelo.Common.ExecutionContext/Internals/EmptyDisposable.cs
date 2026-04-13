using System;

namespace Moedelo.Common.ExecutionContext.Internals;

internal sealed class EmptyDisposable : IDisposable
{
    public static readonly IDisposable Instance = new EmptyDisposable();
        
    public void Dispose()
    {}
}
