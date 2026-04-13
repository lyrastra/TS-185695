using System;
using System.Threading.Tasks;

namespace Moedelo.Common.RabbitMQ.Abstractions
{
    public interface IMoedeloRabbitMqWriter
    {
        Task WriteAsync<T>(string exchangeName, T data, uint repeatCount = 0, TimeSpan? delay = null);
    }
}
