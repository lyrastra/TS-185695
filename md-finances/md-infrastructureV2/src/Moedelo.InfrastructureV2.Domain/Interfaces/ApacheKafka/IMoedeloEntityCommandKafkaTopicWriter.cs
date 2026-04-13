using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

public interface IMoedeloEntityCommandKafkaTopicWriter : IDI
{
    Task<string> WriteCommandDataAsync<T>(string topicName, string commandKey, string entityType, T commandData, string contextToken)
        where T : IEntityCommandData;
}