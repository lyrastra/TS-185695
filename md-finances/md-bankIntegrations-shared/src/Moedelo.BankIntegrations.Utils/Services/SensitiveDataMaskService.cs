using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.Utils.Abstractions.Services;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.BankIntegrations.Utils.Services
{
    [InjectAsSingleton(typeof(ISensitiveDataMaskService))]
    public class SensitiveDataMaskService : ISensitiveDataMaskService
    {
        private readonly ICryptoService cryptoService;
        private readonly ILogger<SensitiveDataMaskService> logger;

        private readonly List<string> sensitiveKeys = new List<string>
            {
                "password",
                "pass",
                "pwd",
                "token",
                "access_token",
                "refresh_token",
                "accessToken",
                "refreshToken",
                "api_key",
                "secret",
                "apikey",
                "key",
                "bearer",
                "client_secret",
                "clientSecret"
            };

        public SensitiveDataMaskService(
            ICryptoService cryptoService,
            ILogger<SensitiveDataMaskService> logger
        )
        {
            this.cryptoService = cryptoService;
            this.logger = logger;
        }
        /// <summary>
        /// Функция находит и шифрует конфиденциальные токены и авторизационные данные в строке, используя регулярные выражения.
        /// </summary>
        public string EncryptToken(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            try
            {
                // Регулярное выражение для обработки чувствительных данных, включая кавычки
                string pattern = @"(?i)([""']?)\b(?<key>" + string.Join("|", sensitiveKeys) + @")\1[\s]*[:=][\s]*[""']?(?<token>[^""'\s,;]+)[""']?";

                string replaced = Regex.Replace(input, pattern, m =>
                {
                    string quote = m.Groups[1].Value;                           // Кавычка вокруг ключа (если есть)
                    string key = m.Groups["key"].Value;                         // Извлекаем ключ ("access_token", "refresh_token" из json и т.д.)
                    string token = m.Groups["token"].Value;                     // Извлекаем токен
                    string encryptedToken = cryptoService.EncryptText(token);   // Шифруем токен

                    // Используем ':' и всегда заключаем значение в двойные кавычки
                    return $"{quote}{key}{quote}: \"{encryptedToken}\"";
                }, RegexOptions.IgnoreCase);

                // Обработка токенов в HTTP-заголовках
                string authPattern = @"(?i)\b(?<key>authorization|auth)[\s]*:[\s]*(?<scheme>Bearer|Basic|Digest|OAuth|JWT|ApiKey|AWS4-HMAC-SHA256|NTLM|Negotiate|Token|Custom)\s+(?<value>\S+)";

                // Заменяем токены, шифруя их с помощью cryptoService
                var replacedAuth = Regex.Replace(replaced, authPattern, match =>
                {
                    string key = match.Groups["key"].Value;                     // Извлекаем ключ (authorization/auth)
                    string scheme = match.Groups["scheme"].Value;               // Извлекаем схему авторизации (Bearer, Basic и т.д.)
                    string token = match.Groups["value"].Value;                 // Извлекаем токен
                    string encryptedToken = cryptoService.EncryptText(token);   // Шифруем токен

                    // Формируем новый заголовок с зашифрованным токеном
                    return $"{key}: {scheme} {encryptedToken}";
                }, RegexOptions.IgnoreCase);
                if (replacedAuth != replaced)
                {
                    replaced = replacedAuth;
                }
                else
                {
                    var authAuthorization = @"(?i)\b(?<key>authorization)[\s]*:[\s]*(?<value>[^\r\n]+)";

                    replaced = Regex.Replace(replaced, authAuthorization, match =>
                    {
                        string key = match.Groups["key"].Value;                     // Извлекаем ключ (authorization/auth)
                        string token = match.Groups["value"].Value;                 // Извлекаем токен
                        string encryptedToken = cryptoService.EncryptText(token);   // Шифруем токен

                        // Формируем новый заголовок с зашифрованным токеном
                        return $"{key}: {encryptedToken}";
                    }, RegexOptions.IgnoreCase);
                }

                return replaced;
            }
            catch (Exception ex)
            {
                logger.LogErrorExtraData(input, $"Ошибка разбора строки регулярным выражением", ex);
                return input;
            }
        }

    }
}