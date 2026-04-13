using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.DbModels;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Models;

namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Abstractions;

internal interface IArchiver
{
    CompressedData Compress(ReadOnlySpan<byte> data, int depth);

    DecompressedData Decompress(
        CompressionType compressionType,
        int depth,
        ReadOnlySpan<byte> data);
}