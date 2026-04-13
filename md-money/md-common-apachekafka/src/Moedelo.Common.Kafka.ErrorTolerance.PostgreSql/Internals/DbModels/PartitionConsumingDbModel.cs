namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.DbModels;

public class PartitionConsumingDbModel
{
    public string ConsumerGroupId { get; set; } = null!;
    public string Topic { get; set; } = null!;
    public int Partition { get; set; }
    public long? CommittedOffset { get; set; }
    public DateTime? CommittedDateUtc { get; set; }
    public int OffsetMapDepth { get; set; }
    public string /*CompressionType*/ DataCompressionType { get; set; } = null!;
    public byte[]? OffsetMapData { get; set; }
}
