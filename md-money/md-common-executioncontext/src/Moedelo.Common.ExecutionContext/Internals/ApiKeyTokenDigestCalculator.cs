using System;
using System.Security.Cryptography;
using System.Text;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.ExecutionContext.Internals;

[InjectAsSingleton(typeof(IApiKeyTokenDigestCalculator))]
internal sealed class ApiKeyTokenDigestCalculator : IApiKeyTokenDigestCalculator
{
    public ApiKeyTokenDigest CalculateDigest(string apiKeyToken)
    {
        var nonce = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;
        using var md5 = MD5.Create();
        var md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(apiKeyToken));
        var tokenMd5 = BytesToString(md5Bytes);
        var digest = CalculateDigest(apiKeyToken, nonce, timestamp);

        return new ApiKeyTokenDigest
        {
            TokenMd5 = tokenMd5,
            Nonce = nonce,
            Timestamp = timestamp,
            Digest = digest
        };
    }

    // ReSharper disable once MemberCanBePrivate.Global - Used by unit tests
    internal static string CalculateDigest(string apiKeyToken, Guid nonce, DateTime timestamp)
    {
        using var sha256 = SHA256.Create();
        var hashPayload = $"{nonce:N}|{timestamp.ToUniversalTime():u}|{apiKeyToken}";
        var digestBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(hashPayload));
        var digest = BytesToString(digestBytes);
        return digest;
    }

    private static string BytesToString(byte[] bytes)
    {
        var sBuilder = new StringBuilder();

        foreach (var value in bytes)
        {
            sBuilder.Append(value.ToString("x2"));
        }

        return sBuilder.ToString();
    }

    public bool CheckDigest(Guid nonce, DateTime timestamp, string digest, string apiKeyToken)
    {
        var calculated = CalculateDigest(apiKeyToken, nonce, timestamp);

        return calculated == digest;
    }
}
