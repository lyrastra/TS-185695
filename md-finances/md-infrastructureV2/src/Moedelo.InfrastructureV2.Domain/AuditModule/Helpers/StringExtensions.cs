using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;

public static class StringExtensions
{
    private static readonly Regex pathToMoedeloProjectDirectory = new Regex(@"^.*?\\(Moedelo\..*)$", RegexOptions.Compiled);
    private static readonly Regex fileName = new Regex(@"^.*\\(\w+).cs$", RegexOptions.Compiled);
        
    public static string NormalizeSourceFilePath(this string sourceFilePath)
    {
        return pathToMoedeloProjectDirectory
            .Replace(sourceFilePath, @"$1")
            .Replace("\\", "/");
    }

    public static string GetSourceFileName(this string sourceFilePath)
    {
        var matches = fileName.Match(sourceFilePath);

        if (matches.Success)
        {
            return matches.Groups[1].Value;
        }

        return null;
    }
    
    // Список чувствительных параметров, которые нужно скрыть
    private static readonly HashSet<string> sensitiveParams = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "password", "pwd", "pass",
        "token", "access_token", "refresh_token",
        "secret", "client_secret", "api_key",
        "code", "authorization_code"
    };

    /// <summary>
    /// Удаляет параметры безопасности из строки URI, например, токен, секрет или пароль
    /// </summary>
    /// <param name="uriOriginalString">Исходная строка URI</param>
    /// <returns>URI без чувствительных параметров</returns>
    public static string MaskSecureParamsInQueryString(this string uriOriginalString)
    {
        if (string.IsNullOrEmpty(uriOriginalString))
        {
            return uriOriginalString;
        }

        try
        {
            var uri = new Uri(uriOriginalString);
            var query = uri.Query;

            if (string.IsNullOrEmpty(query))
            {
                return uriOriginalString;
            }

            var queryParams = HttpUtility.ParseQueryString(query);
            var filteredParams = new NameValueCollection();

            foreach (string key in queryParams.AllKeys.Where(key => key != null))
            {
                filteredParams[key] = sensitiveParams.Contains(key) ? "***" : queryParams[key];
            }

            var filteredQuery = string.Join("&",
                filteredParams.AllKeys.Select(key => $"{key}={filteredParams[key]}"));

            var uriBuilder = new UriBuilder(uri)
            {
                Query = filteredQuery
            };

            return uriBuilder.ToString();
        }
        catch
        {
            // Если не удалось разобрать URI, возвращаем исходную строку
            return uriOriginalString;
        }
    }
}