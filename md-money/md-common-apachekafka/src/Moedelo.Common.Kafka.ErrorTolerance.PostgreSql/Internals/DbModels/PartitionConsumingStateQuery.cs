namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.DbModels;

public record PartitionConsumingStateQuery(string ConsumerGroupId, string Topic, int Partition);

