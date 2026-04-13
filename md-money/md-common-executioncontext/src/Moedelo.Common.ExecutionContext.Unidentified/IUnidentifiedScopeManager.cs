using System;
using System.Threading.Tasks;

namespace Moedelo.Common.ExecutionContext.Unidentified;

public interface IUnidentifiedScopeManager
{
    Task InvokeInUnidentifiedScopeAsync(Func<Task> func);
    ValueTask InvokeInUnidentifiedScopeAsync<T>(Func<T, Task> func, T arg1);
    ValueTask InvokeInUnidentifiedScopeAsync<T1, T2>(Func<T1, T2, Task> func, T1 arg1, T2 arg2);
    ValueTask InvokeInUnidentifiedScopeAsync<T>(Func<T, ValueTask> func, T arg1);
    ValueTask InvokeInUnidentifiedScopeAsync<T1, T2>(Func<T1, T2, ValueTask> func, T1 arg1, T2 arg2);
}