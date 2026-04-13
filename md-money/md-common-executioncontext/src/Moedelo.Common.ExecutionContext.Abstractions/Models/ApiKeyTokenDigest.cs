using System;

namespace Moedelo.Common.ExecutionContext.Abstractions.Models;

public sealed class ApiKeyTokenDigest
{
    public string TokenMd5 { get; set; }
    public Guid Nonce { get; set; }
    public DateTime Timestamp { get; set; }
    public string Digest { get; set; }
}
