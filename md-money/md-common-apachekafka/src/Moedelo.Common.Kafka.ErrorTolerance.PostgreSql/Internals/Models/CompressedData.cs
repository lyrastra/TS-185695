using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.DbModels;

namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Models;

internal readonly ref struct CompressedData
{
    public CompressedData(CompressionType compressionType, int depth, ReadOnlySpan<byte> data)
    {
        CompressionType = compressionType;
        Depth = depth;
        Data = data;
    }

    public CompressionType CompressionType { get; }
    public int Depth { get; }
    public ReadOnlySpan<byte> Data { get; }
}