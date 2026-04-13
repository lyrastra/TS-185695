#nullable enable
using System;

namespace Moedelo.AccountV2.Client.ExternalApi;

public struct ApiKeyTokenDigest
{
    public string TokenMd5 { get; set; }
    public Guid Nonce { get; set; }
    public DateTime Timestamp { get; set; }
    public string Digest { get; set; }
}
