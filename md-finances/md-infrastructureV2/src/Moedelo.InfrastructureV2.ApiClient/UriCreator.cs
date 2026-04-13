using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using System.Web;
using Moedelo.InfrastructureV2.ApiClient.Internals;

namespace Moedelo.InfrastructureV2.ApiClient;

[InjectAsSingleton(typeof(IUriCreator))]
public sealed class UriCreator : IUriCreator
{
    public string Create(string host, string path)
    {
        var query = string.Empty;

        if (!string.IsNullOrEmpty(path))
        {
            var queryIndex = path.IndexOf("?", StringComparison.Ordinal);

            if (queryIndex > 0)
            {
                query = path.Substring(queryIndex + 1);
            }
        }

        return Create(host, path, query);
    }

    public string Create(string host, string path, object queryParams)
    {
        if (queryParams == null)
        {
            return Create(host, path);
        }

        if (queryParams is IEnumerable)
        {
            throw new ArgumentException("Collection parameter is not supported.");
        }

        var properties = queryParams
            .GetType()
            .GetProperties()
            .Select(propertyInfo => new QueryParam(propertyInfo, queryParams))
            .Where(queryParam => !queryParam.IsNullEnumerableQueryParam())
            .Select(queryParam => new KeyValuePair<string, object>(queryParam.Name, queryParam.Value));

        return Create(host, path, properties);
    }

    public string Create(string host, string path, IEnumerable<KeyValuePair<string, object>> queryParams)
    {
        if (queryParams == null)
        {
            return Create(host, path, string.Empty);
        }
        var query = GetQuery(queryParams);

        return Create(host, path, query);
    }

    private static string GetQuery(IEnumerable<KeyValuePair<string, object>> queryParams)
    {
        return string.Join("&", queryParams.Select(FormatParam));
    }

    public string Create(string host, string path, string query)
    {
        var fullPath = host.EndsWith("/") && path.StartsWith("/")
            ? host + path.Substring(1)
            : host + path;
        var uri = new UriBuilder(fullPath) { Query = query };
        return uri.ToString();
    }

    private static string FormatParam(KeyValuePair<string, object> x)
    {
        if (x.Value is Array arr)
        {
            var kv = arr.Cast<object>()
                .Select(item => new KeyValuePair<string, object>(x.Key, item));

            return GetQuery(kv);
        }

        if (x.Value is IEnumerable enumerable and not string)
        {
            var kv = enumerable.Cast<object>()
                .Select(item => new KeyValuePair<string, object>(x.Key, item));

            return GetQuery(kv);
        }

        return x.Key + "=" + HttpUtility.UrlEncode(FormatValue(x.Value));
    }

    private static string FormatValue(object value)
    {
        return value switch
        {
            null => string.Empty,
            DateTime dateTime => GetFormattedDateTime(dateTime),
            decimal or double or float => GetFormattedNumber(value),
            _ => value.ToString()
        };
    }

    private static string GetFormattedDateTime(DateTime value)
    {
        return value
            .ToUniversalTime()
            .ToString("yyyy-MM-ddTHH:mm:ssZ");
    }

    private static string GetFormattedNumber(object value)
    {
        return value?.ToString().Replace(',', '.');
    }
}