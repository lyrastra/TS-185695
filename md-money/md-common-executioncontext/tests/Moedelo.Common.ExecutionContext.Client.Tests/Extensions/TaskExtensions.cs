namespace Moedelo.Common.ExecutionContext.Client.Tests.Extensions;

internal static class TaskExtensions
{
    internal static async ValueTask IgnoreExceptions(this Task task)
    {
        try
        {
            await task;
        }
        catch
        {
            // ignored
        }
    }
}
