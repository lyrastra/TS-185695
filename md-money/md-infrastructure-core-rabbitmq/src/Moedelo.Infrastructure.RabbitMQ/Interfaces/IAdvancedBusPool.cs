using EasyNetQ;

namespace Moedelo.Infrastructure.RabbitMQ.Interfaces
{
    internal interface IAdvancedBusPool
    {
        IAdvancedBus GetAdvancedBus(string connectionString);
    }
}