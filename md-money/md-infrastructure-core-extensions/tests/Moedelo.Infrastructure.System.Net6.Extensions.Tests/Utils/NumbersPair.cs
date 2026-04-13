namespace Moedelo.Infrastructure.System.Net6.Extensions.Tests.Utils;

internal record NumbersPair
{
    public NumbersPair(int n1)
    {
        N1 = n1;
    }

    public int N1 { get; }
    public int N2 { get; set; }
}
