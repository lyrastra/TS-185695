using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

public interface IMoedeloEntityEventKafkaTopicWriter : IDI
{
    Task<string> WriteEventDataAsync<T>(string topicName, string eventKey, string entityType, T eventData, string contextToken)
        where T : IEntityEventData;
}