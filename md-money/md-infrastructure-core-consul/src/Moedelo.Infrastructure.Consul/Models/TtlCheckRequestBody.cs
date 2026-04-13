namespace Moedelo.Infrastructure.Consul.Models;

internal struct TtlCheckRequestBody
{
    public TtlCheckRequestBody(string output)
    {
        Output = output;
    }

    public string Status => "passing";
    public string Output { get; private set; }
}
