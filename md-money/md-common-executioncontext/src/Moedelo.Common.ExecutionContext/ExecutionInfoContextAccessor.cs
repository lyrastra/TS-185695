using System;
using System.Threading;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Common.ExecutionContext.Internals;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.ExecutionContext;

[InjectAsSingleton(typeof(IExecutionInfoContextAccessor))]
internal sealed class ExecutionInfoContextAccessor : IExecutionInfoContextAccessor
{
    private readonly AsyncLocal<ExecutionInfoContextHolder> current = new AsyncLocal<ExecutionInfoContextHolder>();

    public ExecutionInfoContext ExecutionInfoContext => current.Value?.Context;

    public string ExecutionInfoContextToken => current.Value?.Token;

    public IDisposable SetContext(string token, ExecutionInfoContext context)
    {
        var prevValue = current.Value;

        if (context != null)
        {
            _ = token
                ?? throw new ArgumentNullException(nameof(token), "Токен не может быть равен null, если контекст не равен null");
        }

        if (token != null)
        {
            _ = context
                ?? throw new ArgumentNullException(nameof(context), "Контекст не может быть равен null, если токен не равен null");
        }

        var holder = new ExecutionInfoContextHolder(token, context);

        current.Value = holder;

        return new ExecutionContextScope(this.OnDisposeContextScope, holder, prevValue);
    }

    private void OnDisposeContextScope(ExecutionInfoContextHolder currentValue, ExecutionInfoContextHolder previousValue)
    {
        currentValue.Dispose();

        if (previousValue is { IsDisposed: false } && current.Value == currentValue)
        {
            // если удаляется текущий контекст и предыдущий контекст ещё не удалён, то восстанавливаем предыдущий в качестве текущего
            current.Value = previousValue;
        }
    }
}