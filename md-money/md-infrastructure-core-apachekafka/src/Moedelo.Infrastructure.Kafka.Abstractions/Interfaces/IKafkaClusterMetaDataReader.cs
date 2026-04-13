using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Interfaces
{
    public interface IKafkaClusterMetaDataReader
    {
        /// <summary>
        /// Получить список топиков с указанием количества секций для каждого
        /// </summary>
        /// <param name="brokerEndpoints">строка подключения к кластеру kafka</param>
        /// <returns>словарь название_топика:количество_секций</returns>
        Task<IReadOnlyDictionary<string, int>> GetTopicPartitionCountsAsync(string brokerEndpoints);
    }
}