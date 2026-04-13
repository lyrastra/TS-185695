#nullable enable
using System;

namespace Moedelo.AccountV2.Client.ExternalApi;

public interface IApiKeyTokenDigestCalculator
{
    ApiKeyTokenDigest CalculateDigest(string apiKeyToken);
    bool CheckDigest(Guid nonce, DateTime timestamp, string digest, string token);
}