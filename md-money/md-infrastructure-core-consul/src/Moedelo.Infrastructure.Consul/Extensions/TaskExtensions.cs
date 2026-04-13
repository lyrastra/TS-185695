using System;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.Consul.Extensions;

internal static class TaskExtensions
{
    public static void WaitIgnoringException(this Task task, TimeSpan maxWaitingTimeSpan)
    {
        try
        {
            task.Wait(maxWaitingTimeSpan);
        }
        catch
        {
            /* ignoring */
        }
    }

    public static async Task WaitIgnoringExceptionAsync(this Task task, TimeSpan maxWaitingTimeSpan)
    {
        try
        {
            await Task.Yield();
            task.Wait(maxWaitingTimeSpan);
        }
        catch
        {
            /* ignoring */
        }
    }
}
