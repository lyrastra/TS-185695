namespace Moedelo.Common.RabbitMQ.Abstractions
{
    internal interface IMoedeloRabbitMqConfigurator
    {
        string GetExchangeNamePrefix();

        string GetConnection();
    }
}