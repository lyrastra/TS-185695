using System;
using Moedelo.Common.ExecutionContext.Abstractions.Models;

namespace Moedelo.Common.ExecutionContext.Abstractions.Interfaces;

public interface IApiKeyTokenDigestCalculator
{
    ApiKeyTokenDigest CalculateDigest(string apiKeyToken);
    bool CheckDigest(Guid nonce, DateTime timestamp, string digest, string token);
}