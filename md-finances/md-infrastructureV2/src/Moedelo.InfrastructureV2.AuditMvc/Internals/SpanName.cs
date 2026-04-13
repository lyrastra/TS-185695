namespace Moedelo.InfrastructureV2.AuditMvc.Internals;

internal struct SpanName
{
    public SpanName() : this(string.Empty, false)
    {
    }

    public SpanName(string name, bool isNormalized)
    {
        Name = name;
        IsNormalized = isNormalized;
    }

    public bool IsNormalized { get; }

    public string Name { get; }
}
