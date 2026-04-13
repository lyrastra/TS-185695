using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities;

namespace Moedelo.Common.Kafka.Saga.Abstractions
{
    public interface IMoedeloSaga
    {
        Task StartAsync<TInitStateData>(
            TInitStateData initStateData)
            where TInitStateData : ISagaStateData;
        
        /// <summary>
        /// Запустить указанное количество консьюмеров
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IMoedeloSaga WithConsumerCount(int count);

        /// <summary>
        /// Автоматически регулировать количество консьюмеров
        /// </summary>
        /// <returns></returns>
        IMoedeloSaga WithAutoConsumersCount();

        IMoedeloSaga WithOptionalSettings(OptionalReadSettings settings);

        Task RunAsync(string groupId, CancellationToken cancellationToken);
    }
}