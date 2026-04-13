using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Moedelo.Common.Kafka.Monitoring.Extensions
{
    internal static class CancellationTokenExtensions
    {
        /// <summary>
        /// Allows a cancellation token to be awaited.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static CancellationTokenAwaiter GetAwaiter(this CancellationToken ct)
        {
            // return our special awaiter
            return new CancellationTokenAwaiter
            {
                cancellationToken = ct
            };
        }

        /// <summary>
        /// The awaiter for cancellation tokens.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public struct CancellationTokenAwaiter : ICriticalNotifyCompletion
        {
            public CancellationTokenAwaiter(CancellationToken cancellationToken)
            {
                this.cancellationToken = cancellationToken;
            }

            internal CancellationToken cancellationToken;

            public object GetResult()
            {
                // this is called by compiler generated methods when the
                // task has completed. Instead of returning a result, we 
                // just throw an exception.
                if (IsCompleted) throw new OperationCanceledException();
                else throw new InvalidOperationException("The cancellation token has not yet been cancelled.");
            }

            // called by compiler generated/.net internals to check
            // if the task has completed.
            public bool IsCompleted => cancellationToken.IsCancellationRequested;

            // The compiler will generate stuff that hooks in
            // here. We hook those methods directly into the
            // cancellation token.
            public void OnCompleted(Action continuation) =>
                cancellationToken.Register(continuation);

            public void UnsafeOnCompleted(Action continuation) =>
                cancellationToken.Register(continuation);
        }
    }
}