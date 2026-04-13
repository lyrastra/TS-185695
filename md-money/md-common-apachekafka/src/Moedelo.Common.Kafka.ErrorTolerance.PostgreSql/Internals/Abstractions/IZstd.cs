namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Abstractions;

public interface IZstd
{
    int MinBytesToCompress { get; }
    ReadOnlySpan<byte> Compress(ReadOnlySpan<byte> data);
    ReadOnlySpan<byte> Decompress(ReadOnlySpan<byte> data);
}
