#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Infrastructure.Http;

[InjectAsSingleton(typeof(IUriCreator))]
internal sealed class UriCreator : IUriCreator
{
    public string Create(
        string host, 
        string? path, 
        string? query)
    {
        var uri = new UriBuilder(host + (path ?? string.Empty)) { Query = query ?? string.Empty };
            
        return uri.ToString();
    }
        
    public string Create(
        string host, 
        string? path)
    {
        var query = string.Empty;

        if (!string.IsNullOrEmpty(path))
        {
            var queryStartIndex = path.IndexOf("?", StringComparison.Ordinal);

            if (queryStartIndex > 0)
            {
                // todo: а не надо ли тут проверять что это не последний символ?
                // todo: а не надо ли тут вырезать эту часть из path?
                query = path[(queryStartIndex + 1)..];
            }
        }

        return Create(host, path, query);
    }
        
    public string Create(
        string host, 
        string path, 
        IReadOnlyCollection<KeyValuePair<string, object>>? queryParams)
    {
        if (queryParams == null)
        {
            return Create(host, path, string.Empty);
        }

        var formattedParams = queryParams.Select(FormatParam)
            .Where(value => !string.IsNullOrEmpty(value))
            .ToArray();
        var query = string.Join("&", formattedParams);

        return Create(host, path, query);
    }

    public string Create(
        string host, 
        string path, 
        object? queryParams)
    {
        if (queryParams == null)
        {
            return Create(host, path);
        }

        if (queryParams is IEnumerable)
        {
            throw new ArgumentException("Collection parameter is not supported.");
        }

        var properties = queryParams.GetType().GetProperties()
            .ToDictionary(x => x.Name, x => x.GetValue(queryParams, null));

        return Create(host, path, properties);
    }

    private static string FormatParam(KeyValuePair<string, object> namedValue)
    {
        var paramName = namedValue.Key;

        if (namedValue.Value is IEnumerable enumerable && namedValue.Value is not string)
        {
            var kvps = new List<KeyValuePair<string, object>>();
            int index = 0;
            foreach (var item in enumerable)
            {
                if (item != null)
                {
                    if (!item.GetType().IsValueType && item is not string)
                    {
                        foreach (var prop in item.GetType().GetProperties())
                        {
                            kvps.Add(new KeyValuePair<string, object>($"{paramName}[{index}].{prop.Name}", prop.GetValue(item)));
                        }
                    }
                    else
                    {
                        kvps.Add(new KeyValuePair<string, object>(paramName, item));
                    }
                }
                index++;
            }
            return GetQuery(kvps);
        }

        if (namedValue.Value is not null && !namedValue.Value.GetType().IsValueType && namedValue.Value is not string)
        {
            return GetQuery(namedValue.Value.GetType().GetProperties()
                .ToDictionary(
                    propInfo => $"{paramName}.{propInfo.Name}",
                    propInfo => propInfo.GetValue(namedValue.Value)));
        }

        var formattedValue = FormatValue(namedValue.Value);

        return null == formattedValue
            ? string.Empty 
            : $"{paramName}={HttpUtility.UrlEncode(formattedValue)}";
    }

    private static string GetQuery(IEnumerable<KeyValuePair<string, object>> queryParams)
    {
        return string.Join("&", queryParams.Select(FormatParam).Where(value => !string.IsNullOrEmpty(value)).ToArray());
    }
        
    private static string? FormatValue(object? value)
    {
        if (value == null)
        {
            return null;
        }
            
        if (value is DateTime)
        {
            return GetFormattedDateTime(value);
        }
            
        return value.ToString();
    }

    private static string GetFormattedDateTime(object value)
    {
        var dateTime = ((DateTime)value).ToUniversalTime();
        return dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
}