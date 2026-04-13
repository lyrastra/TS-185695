using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Abstractions;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.DbModels;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Mappers;

[InjectAsSingleton(typeof(IDbModelMapper))]
internal sealed class DbModelMapper : IDbModelMapper
{
    private readonly IReadOnlyDictionary<CompressionType, string> enumNames =
        Enum.GetValues<CompressionType>()
            .ToDictionary(value => value, value => value.ToString());

    private readonly IArchiver archiver;

    public DbModelMapper(IArchiver archiver)
    {
        this.archiver = archiver;
    }

    public PartitionConsumingDbModel MapToDbModel(IPartitionConsumingReadOnlyState state)
    {
        var compressed = archiver.Compress(state.OffsetMapData, state.OffsetMapDepth);

        return new()
        {
            ConsumerGroupId = state.ConsumerGroupId,
            Topic = state.Topic,
            Partition = state.Partition,
            CommittedOffset = state.CommittedOffset,
            CommittedDateUtc = state.CommittedDateTimeUtc,
            DataCompressionType = enumNames[compressed.CompressionType],
            OffsetMapDepth = compressed.Depth,
            OffsetMapData = compressed.Data.ToArray(),
        };
    }

    public IPartitionConsumingReadOnlyState MapToDomain(PartitionConsumingDbModel? dbModel,
        PartitionConsumingStateQuery queryParams)
    {
        if (dbModel == null)
        {
            return new PartitionConsumingReadOnlyState(
                queryParams.ConsumerGroupId,
                queryParams.Topic,
                queryParams.Partition,
                CommittedOffset:null,
                CommittedDateTimeUtc:null,
                OffsetMapDepth:0,
                OffsetMapData: Array.Empty<byte>());
        }

        var decompressed = archiver.Decompress(
            Enum.Parse<CompressionType>(dbModel.DataCompressionType),
            dbModel.OffsetMapDepth,
            dbModel.OffsetMapData ?? Array.Empty<byte>());

        return new PartitionConsumingReadOnlyState(
            dbModel.ConsumerGroupId,
            dbModel.Topic,
            dbModel.Partition,
            dbModel.CommittedOffset,
            dbModel.CommittedDateUtc,
            decompressed.Depth,
            decompressed.Data.ToArray());
    }
}
