using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Moedelo.Common.Settings.Crypto;

internal static class Decryptor
{
    private const string EncryptedPattern = "^encrypted_(?<algorithm>\\d+)_(?<keyId>.+)_(?<nonce>.+)_(?<value>.+)$";
    private const string CryptoKeyPrefix = "CryptoKey_";

    private static readonly ConcurrentDictionary<string, string> Keys = new();
    
    public static bool TryDecrypt(string value, out string decryptedValue)
    {
        decryptedValue = null;
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }
        
        var match = Regex.Match(value, EncryptedPattern);
        if (!match.Success)
        {
            return false;
        }

        var algorithmId = match.Groups["algorithm"].Value;
        var keyId = match.Groups["keyId"].Value;
        var encryptedValue = match.Groups["value"].Value;
        var nonce = match.Groups["nonce"].Value;

        if (!Enum.TryParse(algorithmId, out Algorithm.AlgorithmType algorithm))
        {
            throw new Exception($"Unknown crypto algorithm {algorithmId}");
        }

        var key = Keys.GetOrAdd(keyId, LoadKey);

        if (string.IsNullOrEmpty(key))
        {
            throw new Exception($"Can't get crypto key by id {keyId}");
        }

        decryptedValue = encryptedValue.Decrypt(algorithm, key, nonce);
        return true;
    }

    private static string LoadKey(string keyId) => Environment
        .GetEnvironmentVariable(
            $"{CryptoKeyPrefix}{keyId}",
            EnvironmentVariableTarget.Machine);
}