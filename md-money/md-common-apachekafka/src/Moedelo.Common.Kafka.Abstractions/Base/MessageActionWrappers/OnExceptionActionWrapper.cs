using System;
using System.Threading.Tasks;

namespace Moedelo.Common.Kafka.Abstractions.Base.MessageActionWrappers
{
    public sealed class OnExceptionActionWrapper<T> : IMessageActionWrapper<T>
        where T : MoedeloKafkaMessageValueBase
    {
        private readonly Func<T, Exception, Task> onFatalException;

        public OnExceptionActionWrapper(
            Func<T, Exception, Task> onFatalException)
        {
            this.onFatalException = onFatalException;
        }

        public Func<T, Task> Wrap(Func<T, Task> onMessage)
        {
            return async message =>
            {
                try
                {
                    await onMessage(message).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    if (onFatalException != null)
                    {
                        await onFatalException(message, ex).ConfigureAwait(false);
                    }

                    throw;
                }
            };
        }
    }
}