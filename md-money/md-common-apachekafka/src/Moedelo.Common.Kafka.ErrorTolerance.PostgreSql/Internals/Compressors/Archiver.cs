using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Abstractions;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.DbModels;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Compressors;

[InjectAsSingleton(typeof(IArchiver))]
internal sealed class Archiver : IArchiver
{
    private readonly IZstd zstd;

    public Archiver(IZstd zstd)
    {
        this.zstd = zstd;
    }

    public CompressedData Compress(ReadOnlySpan<byte> data, int depth)
    {
        if (depth < zstd.MinBytesToCompress)
        {
            return new(CompressionType.Raw, depth, data[..depth]);
        }

        var compressed = zstd.Compress(data);

        if (compressed.Length < data.Length)
        {
            return new(CompressionType.Zstd, depth, compressed);
        }
        
        return new(CompressionType.Raw, depth, data);
    }

    public DecompressedData Decompress(
        CompressionType compressionType,
        int depth,
        ReadOnlySpan<byte> data)
    {
        return compressionType switch
        {
            CompressionType.Raw => new (depth, data),
            CompressionType.Zstd => new (depth, zstd.Decompress(data)),
            _ => throw new ArgumentOutOfRangeException(nameof(compressionType), compressionType, null)
        };
    }
}
