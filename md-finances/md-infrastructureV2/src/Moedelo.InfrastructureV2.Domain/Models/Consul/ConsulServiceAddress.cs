namespace Moedelo.InfrastructureV2.Domain.Models.Consul;

public class ConsulServiceAddress
{
    public ConsulServiceAddress(string address, int port)
    {
        Address = address;
        Port = port;
    }

    public string Address { get; }
    public int Port { get; }
}