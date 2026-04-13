using System;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Models;

namespace Moedelo.Common.ExecutionContext.Abstractions.Interfaces;

public static class ExecutionInfoContextAccessorExtensions
{
    public static void RunInContext(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Action action)
    {
        using var _ = contextAccessor.SetContext(token, context);

        action.Invoke();
    }

    public static void RunInContext<TArg1>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Action<TArg1> action, TArg1 arg)
    {
        using var _ = contextAccessor.SetContext(token, context);

        action.Invoke(arg);
    }

    public static void RunInContext<TArg1, TArg2>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Action<TArg1, TArg2> action, TArg1 arg1, TArg2 arg2)
    {
        using var _ = contextAccessor.SetContext(token, context);

        action.Invoke(arg1, arg2);
    }

    public static void RunInContext<TArg1, TArg2, TArg3>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Action<TArg1, TArg2, TArg3> action, TArg1 arg1, TArg2 arg2, TArg3 arg3)
    {
        using var _ = contextAccessor.SetContext(token, context);

        action.Invoke(arg1, arg2, arg3);
    }
    
    public static TResult RunInContext<TResult>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TResult> action)
    {
        using var _ = contextAccessor.SetContext(token, context);

        return action.Invoke();
    }

    public static TResult RunInContext<TArg1, TResult>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, TResult> action, TArg1 arg)
    {
        using var _ = contextAccessor.SetContext(token, context);

        return action.Invoke(arg);
    }

    public static TResult RunInContext<TArg1, TArg2, TResult>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, TArg2, TResult> action, TArg1 arg1, TArg2 arg2)
    {
        using var _ = contextAccessor.SetContext(token, context);

        return action.Invoke(arg1, arg2);
    }

    public static TResult RunInContext<TArg1, TArg2, TArg3, TResult>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, TArg2, TArg3, TResult> action, TArg1 arg1, TArg2 arg2, TArg3 arg3)
    {
        using var _ = contextAccessor.SetContext(token, context);

        return action.Invoke(arg1, arg2, arg3);
    }

    public static async Task RunInContextAsync(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<Task> action)
    {
        using var _ = contextAccessor.SetContext(token, context);

        await action.Invoke();
    }

    public static async Task RunInContextAsync<TArg1>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, Task> action, TArg1 arg)
    {
        using var _ = contextAccessor.SetContext(token, context);

        await action.Invoke(arg);
    }

    public static async Task RunInContextAsync<TArg1, TArg2>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, TArg2, Task> action, TArg1 arg1, TArg2 arg2)
    {
        using var _ = contextAccessor.SetContext(token, context);

        await action.Invoke(arg1, arg2);
    }

    public static async Task RunInContextAsync<TArg1, TArg2, TArg3>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, TArg2, TArg3, Task> action, TArg1 arg1, TArg2 arg2, TArg3 arg3)
    {
        using var _ = contextAccessor.SetContext(token, context);

        await action.Invoke(arg1, arg2, arg3);
    }

    public static async ValueTask RunInContextAsync(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<ValueTask> action)
    {
        using var _ = contextAccessor.SetContext(token, context);

        await action.Invoke();
    }

    public static async ValueTask RunInContextAsync<TArg1>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, ValueTask> action, TArg1 arg)
    {
        using var _ = contextAccessor.SetContext(token, context);

        await action.Invoke(arg);
    }

    public static async ValueTask RunInContextAsync<TArg1, TArg2>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, TArg2, ValueTask> action, TArg1 arg1, TArg2 arg2)
    {
        using var _ = contextAccessor.SetContext(token, context);

        await action.Invoke(arg1, arg2);
    }

    public static async ValueTask RunInContextAsync<TArg1, TArg2, TArg3>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, TArg2, TArg3, ValueTask> action, TArg1 arg1, TArg2 arg2, TArg3 arg3)
    {
        using var _ = contextAccessor.SetContext(token, context);

        await action.Invoke(arg1, arg2, arg3);
    }

    public static async Task<TResult> RunInContextAsync<TResult>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<Task<TResult>> action)
    {
        using var _ = contextAccessor.SetContext(token, context);

        return await action.Invoke();
    }

    public static async Task<TResult> RunInContextAsync<TArg1, TResult>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, Task<TResult>> action, TArg1 arg)
    {
        using var _ = contextAccessor.SetContext(token, context);

        return await action.Invoke(arg);
    }

    public static async Task<TResult> RunInContextAsync<TArg1, TArg2, TResult>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, TArg2, Task<TResult>> action, TArg1 arg1, TArg2 arg2)
    {
        using var _ = contextAccessor.SetContext(token, context);

        return await action.Invoke(arg1, arg2);
    }

    public static async Task<TResult> RunInContextAsync<TArg1, TArg2, TArg3, TResult>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, TArg2, TArg3, Task<TResult>> action, TArg1 arg1, TArg2 arg2, TArg3 arg3)
    {
        using var _ = contextAccessor.SetContext(token, context);

        return await action.Invoke(arg1, arg2, arg3);
    }

    public static async ValueTask<TResult> RunInContextAsync<TResult>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<ValueTask<TResult>> action)
    {
        using var _ = contextAccessor.SetContext(token, context);

        return await action.Invoke();
    }

    public static async ValueTask<TResult> RunInContextAsync<TArg1, TResult>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, ValueTask<TResult>> action, TArg1 arg)
    {
        using var _ = contextAccessor.SetContext(token, context);

        return await action.Invoke(arg);
    }

    public static async ValueTask<TResult> RunInContextAsync<TArg1, TArg2, TResult>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, TArg2, ValueTask<TResult>> action, TArg1 arg1, TArg2 arg2)
    {
        using var _ = contextAccessor.SetContext(token, context);

        return await action.Invoke(arg1, arg2);
    }

    public static async ValueTask<TResult> RunInContextAsync<TArg1, TArg2, TArg3, TResult>(this IExecutionInfoContextAccessor contextAccessor,
        string token, ExecutionInfoContext context, Func<TArg1, TArg2, TArg3, ValueTask<TResult>> action, TArg1 arg1, TArg2 arg2, TArg3 arg3)
    {
        using var _ = contextAccessor.SetContext(token, context);

        return await action.Invoke(arg1, arg2, arg3);
    }
}
