using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Common.ExecutionContext.Client;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.ExecutionContext.Unidentified;

[InjectAsSingleton(typeof(IUnidentifiedScopeManager))]
internal sealed class UnidentifiedScopeManager : IUnidentifiedScopeManager
{
    private static readonly TimeSpan CacheValidity = TimeSpan.FromSeconds(30);

    private readonly IExecutionContextApiClient executionContextApiClient;
    private readonly IExecutionInfoContextInitializer contextInitializer;
    private readonly IExecutionInfoContextAccessor contextAccessor;
    private ExecutionContextData unidentifiedData = new (null, new ExecutionInfoContext(), DateTime.MinValue);

    public UnidentifiedScopeManager(
        IExecutionContextApiClient executionContextApiClient,
        IExecutionInfoContextInitializer contextInitializer, 
        IExecutionInfoContextAccessor contextAccessor)
    {
        this.executionContextApiClient = executionContextApiClient;
        this.contextInitializer = contextInitializer;
        this.contextAccessor = contextAccessor;
    }
        
    public async Task InvokeInUnidentifiedScopeAsync(Func<Task> func)
    {
        var (token, executionInfoContext, _) = await CreateUnidentifiedTokenDataAsync();

        await contextAccessor.RunInContextAsync(token, executionInfoContext, func);
    }

    public async ValueTask InvokeInUnidentifiedScopeAsync<T>(Func<T, Task> func, T arg1)
    {
        var (token, executionInfoContext, _) = await CreateUnidentifiedTokenDataAsync();
        
        await contextAccessor.RunInContextAsync(token, executionInfoContext, func, arg1);
    }

    public async ValueTask InvokeInUnidentifiedScopeAsync<T1, T2>(Func<T1, T2, Task> func, T1 arg1, T2 arg2)
    {
        var (token, executionInfoContext, _) = await CreateUnidentifiedTokenDataAsync();

        await contextAccessor.RunInContextAsync(token, executionInfoContext, func, arg1, arg2);
    }

    public async ValueTask InvokeInUnidentifiedScopeAsync<T>(Func<T, ValueTask> func, T arg1)
    {
        var (token, executionInfoContext, _) = await CreateUnidentifiedTokenDataAsync();

        await contextAccessor.RunInContextAsync(token, executionInfoContext, func, arg1);
    }

    public async ValueTask InvokeInUnidentifiedScopeAsync<T1, T2>(Func<T1, T2, ValueTask> func, T1 arg1, T2 arg2)
    {
        var (token, executionInfoContext, _) = await CreateUnidentifiedTokenDataAsync();

        await contextAccessor.RunInContextAsync(token, executionInfoContext, func, arg1, arg2);
    }

    private async ValueTask<ExecutionContextData> CreateUnidentifiedTokenDataAsync()
    {
        var now = DateTime.Now;

        if (unidentifiedData.ValidUntil < now)
        {
            // мы можем сюда попасть одновременно из разных потоков - ничего страшного, нет смысла в синхронизации
            var token = await executionContextApiClient.GetUnidentifiedTokenAsync(CancellationToken.None);
            var executionInfoContext = contextInitializer.Initialize(token);
            var validUntil = now.Add(CacheValidity);

            unidentifiedData = new(token, executionInfoContext, validUntil);
        }

        return unidentifiedData;
    }
}