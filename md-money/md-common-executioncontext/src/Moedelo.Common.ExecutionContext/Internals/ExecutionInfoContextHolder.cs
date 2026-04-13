using System;
using Moedelo.Common.ExecutionContext.Abstractions.Models;

namespace Moedelo.Common.ExecutionContext.Internals;

internal sealed class ExecutionInfoContextHolder : IDisposable
{
    public ExecutionInfoContextHolder(string token, ExecutionInfoContext context)
    {
        Token = token;
        Context = context;
        IsDisposed = false;
    }

    public string Token { get; private set; }
    public ExecutionInfoContext Context { get; private set; }
    public bool IsDisposed { get; private set; }

    public void Dispose()
    {
        Token = null;
        Context = null;
        IsDisposed = true;
    }
}
