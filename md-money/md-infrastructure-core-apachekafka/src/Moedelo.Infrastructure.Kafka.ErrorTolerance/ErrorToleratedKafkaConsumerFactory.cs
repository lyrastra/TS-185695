using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Implementations;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

[InjectAsSingleton(typeof(IErrorToleratedKafkaConsumerFactory))]
[InjectAsSingleton(typeof(IKafkaConsumerFactory))]
internal sealed class ErrorToleratedKafkaConsumerFactory : AbstractKafkaConsumerFactory, IErrorToleratedKafkaConsumerFactory
{
    private readonly IConfluentConsumerFactory confluentConsumerFactory;
    private readonly IPartitionConsumingStateFactory partitionStateFactory;
    private readonly IKafkaConsumerMessageMemoryRepository[] memoryRepositories;
    private readonly IPartitionStateEstimator stateEstimator;

    public ErrorToleratedKafkaConsumerFactory(
        IConfluentConsumerFactory confluentConsumerFactory,
        IPartitionConsumingStateFactory partitionStateFactory,
        IEnumerable<IKafkaConsumerMessageMemoryRepository> memoryRepositories,
        IPartitionStateEstimator stateEstimator)
    {
        this.confluentConsumerFactory = confluentConsumerFactory;
        this.partitionStateFactory = partitionStateFactory;
        this.stateEstimator = stateEstimator;
        this.memoryRepositories = memoryRepositories.ToArray();
    }

    protected override IKafkaConsumer Create(KafkaConsumerConfig config, ILogger logger)
    {
        var options = config.ExtraOptions.GetRequired<ConsumingErrorToleranceOptions>();
        var memoryRepositoryType = options.KafkaConsumerMessageMemoryRepositoryType
            .EnsureIsNotNull(nameof(ConsumingErrorToleranceOptions.KafkaConsumerMessageMemoryRepositoryType));

        var memoryRepository = memoryRepositories
            .SingleOrDefault(memoryRepositoryType.IsInstanceOfType)
            .EnsureIsNotNull("memoryRepository",
                () => $"Не удалось найти экземпляр репозитория типа {memoryRepositoryType.Name}. Проверьте подключена ли сборка {memoryRepositoryType.Assembly.FullName} к проекту приложения");

        var memory = new KafkaConsumerStateMemory(
            memoryRepository,
            new ConsumingStateMemory(config.GroupId.Raw, partitionStateFactory, options.MaxOffsetMapDepth));

        return new ErrorToleratedKafkaConsumer(config, options, memory, stateEstimator, confluentConsumerFactory, logger);
    }
}
