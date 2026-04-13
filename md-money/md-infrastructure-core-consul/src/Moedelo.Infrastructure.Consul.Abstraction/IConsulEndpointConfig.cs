namespace Moedelo.Infrastructure.Consul.Abstraction
{
    public interface IConsulEndpointConfig
    {
        ConsulServiceAddress GetConfig();
    }
}