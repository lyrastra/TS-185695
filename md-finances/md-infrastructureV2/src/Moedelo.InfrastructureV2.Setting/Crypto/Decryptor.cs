using System;
using System.Text.RegularExpressions;

namespace Moedelo.InfrastructureV2.Setting.Crypto
{

    internal static class Decryptor
    {
        private const string EncryptedPattern = "^encrypted_(?<algorythm>\\d+)_(?<keyId>.+)_(?<nonce>.+)_(?<value>.+)$";
        private const string CryptoKeyPrefix = "CryptoKey_";

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

            var algorythmId = match.Groups["algorythm"].Value;
            var keyId = match.Groups["keyId"].Value;
            var encryptedValue = match.Groups["value"].Value;
            var nonce = match.Groups["nonce"].Value;

            if (!Enum.TryParse(algorythmId, out Algorythm.AlgorythmType algorythm))
            {
                throw new Exception($"Unknown crypto algorythm {algorythmId}");
            }

            var key = Environment.GetEnvironmentVariable($"{CryptoKeyPrefix}{keyId}", 
                EnvironmentVariableTarget.Machine);
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception($"Can't get crypto key by id {keyId}");
            }

            decryptedValue = encryptedValue.Decrypt(algorythm, key, nonce);
            return true;
        }
    }
}