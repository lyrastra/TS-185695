using System;
using System.Threading.Tasks;

namespace Moedelo.Common.Kafka.Abstractions.Base
{
    public static class MessageActionWrapperExtensions
    {
        public static Func<T, Task> WrapBy<T>(
            this Func<T, Task> onMessage,
            IMessageActionWrapper<T> wrapper)
            where T : MoedeloKafkaMessageValueBase
        {
            return wrapper.Wrap(onMessage);
        }

        public static Func<T, Task> WrapByIf<T>(
            this Func<T, Task> onMessage,
            IMessageActionWrapper<T> wrapper,
            bool predicate)
            where T : MoedeloKafkaMessageValueBase
        {
            return predicate ? wrapper.Wrap(onMessage) : onMessage;
        }

        public static Func<T, Task> WrapByIf<T>(
            this Func<T, Task> onMessage,
            bool condition,
            Func<IMessageActionWrapper<T>> wrapperFactory)
            where T : MoedeloKafkaMessageValueBase
        {
            return condition
                ? (wrapperFactory ?? throw new ArgumentException(nameof(wrapperFactory))).Invoke().Wrap(onMessage)
                : onMessage;
        }
    }
}