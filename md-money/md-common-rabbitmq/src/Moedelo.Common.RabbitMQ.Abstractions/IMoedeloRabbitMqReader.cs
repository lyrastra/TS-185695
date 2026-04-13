using System;
using System.Threading.Tasks;

namespace Moedelo.Common.RabbitMQ.Abstractions
{
    public interface IMoedeloRabbitMqReader
    {
        Task ReadWithRetryAsync<T>(
            string groupId,
            string exchangeName,
            Func<T, Task> onMessage,
            Func<T, Exception, Task> onException = null,
            ReadWithRetryOptions options = null);
        
        Task ReadWithRetryAsync<T>(
            string groupId,
            string exchangeName,
            Func<T, uint, Task> onMessage,
            Func<T, Exception, Task> onException = null,
            ReadWithRetryOptions options = null);
    }
}