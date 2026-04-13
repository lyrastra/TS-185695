using System;
using System.Threading;

namespace Moedelo.InfrastructureV2.Logging.Helper
{
    public class AsyncLocalLazy<T>
        where T : class
    {
        private AsyncLocal<T> local = new AsyncLocal<T>();
        private Func<T> init;

        public AsyncLocalLazy(Func<T> init)
        {
            this.init = init;
        }

        public T GetOrCreateValue()
        {
            return local.Value ?? Get();
        }

        public T GetValue()
        {
            return local.Value;
        }

        private T Get()
        {
            T value;
            lock (local)
            {
                value = local.Value;
                if (value == null)
                {
                    value = init?.Invoke();
                    local.Value = value;
                }
            }
            return value;
        }
    }
}