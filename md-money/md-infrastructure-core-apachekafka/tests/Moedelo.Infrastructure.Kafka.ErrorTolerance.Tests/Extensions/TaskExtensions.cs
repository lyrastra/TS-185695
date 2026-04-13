namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests.Extensions;

internal static class TaskExtensions
{
    internal static async ValueTask IgnoreException<TException, TResult>(
        this ValueTask<TResult> task) where TException: Exception
    {
        try
        {
            await task;
        }
        catch (TException)
        {
        }
    }
}
