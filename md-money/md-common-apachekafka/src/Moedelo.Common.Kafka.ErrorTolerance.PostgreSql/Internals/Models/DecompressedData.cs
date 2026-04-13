namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Models;

internal readonly ref struct DecompressedData
{
    public DecompressedData(int depth, ReadOnlySpan<byte> data)
    {
        Depth = depth;
        Data = data;
    }
    
    public int Depth { get; }
    public ReadOnlySpan<byte> Data { get; }
}
