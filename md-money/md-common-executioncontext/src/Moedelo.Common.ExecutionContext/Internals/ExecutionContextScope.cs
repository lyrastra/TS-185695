#nullable enable
using System;

namespace Moedelo.Common.ExecutionContext.Internals;

internal sealed class ExecutionContextScope : IDisposable
{
    private readonly Action<ExecutionInfoContextHolder,ExecutionInfoContextHolder?> onDispose;
    private readonly ExecutionInfoContextHolder current;
    private readonly ExecutionInfoContextHolder? previous;

    public ExecutionContextScope(
        Action<ExecutionInfoContextHolder, ExecutionInfoContextHolder?> onDispose,
        ExecutionInfoContextHolder current,
        ExecutionInfoContextHolder? previous)
    {
        this.onDispose = onDispose;
        this.current = current ?? throw new ArgumentNullException(nameof(current), "Нельзя установить null");
        this.previous = previous;
    }

    public void Dispose()
    {
        onDispose.Invoke(current, previous);
    }
}
