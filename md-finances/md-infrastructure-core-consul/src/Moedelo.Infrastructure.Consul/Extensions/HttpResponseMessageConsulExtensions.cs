using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Consul.Internals;

namespace Moedelo.Infrastructure.Consul.Extensions;

internal static class HttpResponseMessageConsulExtensions
{
    internal static async Task<IReadOnlyCollection<KeyValuePair<string, string>>> BuildConsulKeyValueCollectionAsync(
        this HttpResponseMessage response,
        string keyPath)
    {
        if (response.IsSuccessStatusCode == false)
        {
            return null;
        }

        var keyValues = await response
            .DeserializeJsonContentAsync<KeyValue[]>()
            .ConfigureAwait(false);

        return keyValues?
            .Select(keyValue =>
            {
                var key = keyValue.Key.Substring(keyPath.Length).ConvertConsulPathToCodeKeyPath();
                var value = keyValue.Value?.GetUtf8StringFromBase64String();

                return new KeyValuePair<string, string>(key, value);
            })
            .ToList();
    }

    internal static async Task<string> GetConsulValueKeyAsync(this HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode == false)
        {
            return null;
        }

        var keyValues = await response
            .DeserializeJsonContentAsync<KeyValue[]>(cancellationToken)
            .ConfigureAwait(false);

        if (keyValues == null || keyValues.Length == 0)
        {
            return null;
        }

        return keyValues[0].Value?.GetUtf8StringFromBase64String();
    }
    
    internal static long TryGetConsulIndex(this HttpResponseMessage response)
    {
        const string xConsulIndexHeader = "X-Consul-Index";
        const long defaultIndex = ConsulQueryParams.UndefinedIndex;

        if (response.Headers.TryGetValues(xConsulIndexHeader, out var headerValues) == false)
        {
            return defaultIndex;
        }

        var headerValue = headerValues.FirstOrDefault();

        if (headerValue == null)
        {
            return defaultIndex;
        }

        return long.TryParse(headerValue, out var index) ? index : defaultIndex;
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private struct KeyValue
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
