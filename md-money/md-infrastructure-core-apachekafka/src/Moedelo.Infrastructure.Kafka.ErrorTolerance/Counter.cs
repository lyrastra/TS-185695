using System.Threading;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

internal sealed class Counter
{
    private int value;

    public Counter()
    {
        value = 1;
    }

    public int Value => value;

    public void Increment()
    {
        Interlocked.Increment(ref value);
    }

    public int Decrement()
    {
        return Interlocked.Decrement(ref value);
    }
}
