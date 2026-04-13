using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using ZstdSharp;

namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Compressors;

[InjectAsSingleton(typeof(IZstd))]
internal sealed class Zstd : IZstd
{
    private const int CompressionLevel = 4;
    public int MinBytesToCompress => 128;

    public ReadOnlySpan<byte> Compress(ReadOnlySpan<byte> data)
    {
        using var compressor = new Compressor(CompressionLevel);

        return compressor.Wrap(data);
    }

    public ReadOnlySpan<byte> Decompress(ReadOnlySpan<byte> data)
    {
        using var decompressor = new Decompressor();

        return decompressor.Unwrap(data);
    }
}
