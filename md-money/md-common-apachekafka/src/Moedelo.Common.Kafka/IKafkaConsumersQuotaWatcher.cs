using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka;

public interface IKafkaConsumersQuotaWatcher
{
    ServiceKafkaBalanceContext CreateServiceKafkaBalanceContext(
        KafkaConsumerConfig kafkaConsumerSettings,
        string serviceId);

    Task WatchQuotaChangesAsync(
        ServiceKafkaBalanceContext context,
        Func<ServiceKafkaBalanceState, Task> onQuotaChanged,
        CancellationToken cancellationToken);
}
