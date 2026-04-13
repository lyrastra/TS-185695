using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.DbModels;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Abstractions;

internal interface IDbModelMapper
{
    PartitionConsumingDbModel MapToDbModel(IPartitionConsumingReadOnlyState partitionConsumingState);
    IPartitionConsumingReadOnlyState MapToDomain(PartitionConsumingDbModel? dbModel, PartitionConsumingStateQuery queryParams);
}
