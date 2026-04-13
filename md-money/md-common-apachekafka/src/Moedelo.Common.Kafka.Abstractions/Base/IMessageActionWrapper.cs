using System;
using System.Threading.Tasks;

namespace Moedelo.Common.Kafka.Abstractions.Base
{
    public interface IMessageActionWrapper<T> where T : MoedeloKafkaMessageValueBase
    {
        Func<T, Task> Wrap(Func<T, Task> onMessage);
    }
}