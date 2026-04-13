using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business
{
    public class AsyncLazy<T> : Lazy<Task<T>>
    {
        public AsyncLazy(Func<Task<T>> taskFactory) :
            base(() => taskFactory())
        { }

        public TaskAwaiter<T> GetAwaiter() => Value.GetAwaiter();

        public ConfiguredTaskAwaitable<T> ConfigureAwait(bool continueOnCapturedContext) => Value.ConfigureAwait(continueOnCapturedContext);
    }
}
