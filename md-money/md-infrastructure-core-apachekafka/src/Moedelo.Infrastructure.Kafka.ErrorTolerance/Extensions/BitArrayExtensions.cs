using System.Collections;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;

internal static class BitArrayExtensions
{
    internal static byte[] ToByteArray(this BitArray bits)
    {
        if (bits.Length == 0)
        {
            return Array.Empty<byte>();
        }

        var ret = new byte[(bits.Length - 1) / 8 + 1];
        bits.CopyTo(ret, 0);

        return ret;
    }
}
