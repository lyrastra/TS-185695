using Confluent.Kafka;

namespace Moedelo.Infrastructure.Kafka.Extensions;

internal static class ConsumerBuilderExtensions
{
    internal static ConsumerBuilder<string, string> SetHandlers(
        this ConsumerBuilder<string, string> builder,
        ConfluentConsumerEventHandlers handlers)
    {
        builder.SetErrorHandler(handlers.OnError);
        if (handlers.OnPartitionAssigned != null)
        {
            builder.SetPartitionsAssignedHandler(handlers.OnPartitionAssigned);
        }

        if (handlers.OnPartitionRevoked != null)
        {
            builder.SetPartitionsRevokedHandler(handlers.OnPartitionRevoked);
        }

        return builder;
    }
}
