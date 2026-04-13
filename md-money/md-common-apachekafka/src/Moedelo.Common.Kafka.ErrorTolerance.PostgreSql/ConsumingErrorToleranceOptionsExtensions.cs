using System.Diagnostics;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance;

namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;

public static class ConsumingErrorToleranceOptionsExtensions
{
    public static ConsumingErrorToleranceOptions UsePostgresMemoryStorage(this ConsumingErrorToleranceOptions options)
    {
        Debug.Assert(options.KafkaConsumerMessageMemoryRepositoryType is null);
        options.KafkaConsumerMessageMemoryRepositoryType = typeof(IMoedeloKafkaConsumerMessageMemoryPostgresRepository);

        return options;
    }
}
