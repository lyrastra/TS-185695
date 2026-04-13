using System.Collections;
using FluentAssertions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests;

[TestFixture]
[Parallelizable(ParallelScope.Children)]
public class BitArrayExtensionsTests
{
    [Test]
    public void KeepData()
    {
        var expected = new byte[1024];
        TestContext.CurrentContext.Random.NextBytes(expected);

        var bits = new BitArray(expected);

        var actual = bits.ToByteArray();

        actual.Should().BeEquivalentTo(expected);
    }
}
