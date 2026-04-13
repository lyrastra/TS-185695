using System.Collections;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests.Extensions;

internal static class BitArrayExtensions
{
    internal static void CopyFrom(this BitArray bitArray, IEnumerable<bool> values)
    {
        foreach (var (value, index) in values.Select((value, index) => (value, index)))
        {
            bitArray[index] = value;
        }
    }
}
