using Confluent.Kafka;

namespace Moedelo.Infrastructure.Kafka.Abstractions;

internal interface IAdminClientPool
{
    IAdminClient GetAdminClient(string brokerEndpoints);
}
