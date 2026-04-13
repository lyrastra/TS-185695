using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.Consul.Extensions;

internal static class DisposableExtensions
{
    internal static void DisposeIgnoringExceptions(this IEnumerable<IDisposable> disposables)
    {
        foreach (var disposable in disposables)
        {
            try
            {
                disposable.Dispose();
            }
            catch
            {
                ; // игнорируем ошибки
            }
        }
    }
    
    internal static async ValueTask DisposeIgnoringExceptionsAsync(this IEnumerable<IAsyncDisposable> disposables)
    {
        try
        {
            await Task
                .WhenAll(disposables.Select(async task => await task.DisposeAsync().ConfigureAwait(false)))
                .ConfigureAwait(false);
        }
        catch
        {
            // ошибки игнорируем, чтобы не получать лишних записей в системный Event Log
        }
    }
}
