using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Common.Kafka.Abstractions
{
    public interface IMoedeloKafkaClusterMetaDataReader
    {
        /// <summary>
        /// Получить список топиков с указанием количества секций для каждого
        /// </summary>
        /// <returns>словарь название_топика:количество_секций</returns>
        Task<IReadOnlyDictionary<string, int>> GetTopicPartitionCountsAsync(bool removeEnvPrefix = true);
    }
}