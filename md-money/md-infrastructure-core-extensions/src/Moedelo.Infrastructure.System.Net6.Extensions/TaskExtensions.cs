using System;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.System.Extensions
{
    public static class TaskExtensions
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
                await task.WaitAsync(maxWaitingTimeSpan).ConfigureAwait(false);
            }
            catch
            {
                /* ignoring */
            }
        }
    }
}
