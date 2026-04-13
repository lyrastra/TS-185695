using System;
using System.IO;
using Moedelo.Infrastructure.Consul.Abstraction;

namespace Moedelo.Infrastructure.Consul.Extensions;

internal static class ConsulEndpointConfigExtensions
{
    private const string HttpScheme = "http";
    
    internal static string GetKeyUrl(this ConsulServiceAddress address, string keyPath)
    {
        return address.GetKeyUrl(keyPath, string.Empty);
    }

    internal static string GetKeyUrl(this ConsulServiceAddress address, string keyPath, string uriQuery)
    {
        return address.GetPathUrl("v1/kv", keyPath, uriQuery);
    }

    internal static string GetAgentApiMethodUrl(this ConsulServiceAddress address, string methodPath)
    {
        return address.GetAgentApiMethodUrl(methodPath, string.Empty);
    }

    internal static string GetAgentApiMethodUrl(this ConsulServiceAddress address, string methodPath, string uriQuery)
    {
        return address.GetPathUrl("v1/agent", methodPath, uriQuery);
    }

    internal static string GetCatalogApiMethodUrl(this ConsulServiceAddress address, string methodPath)
    {
        return address.GetCatalogApiMethodUrl(methodPath, string.Empty);
    }

    internal static string GetCatalogApiMethodUrl(this ConsulServiceAddress address, string methodPath, string uriQuery)
    {
        return address.GetPathUrl("v1/catalog", methodPath, uriQuery);
    }

    internal static string GetSessionApiMethodUrl(this ConsulServiceAddress address, string methodPath)
    {
        return address.GetPathUrl("v1/session", methodPath, uriQuery: string.Empty);
    }

    private static string GetPathUrl(this ConsulServiceAddress address, string pathPrefix, string pathSuffix, string uriQuery)
    {
        var path = Path.Combine(pathPrefix, pathSuffix.TrimStart('/'))
            .Replace('\\', '/');
        var uriBuilder = new UriBuilder(HttpScheme, address.Host, address.Port, path)
        {
            Query = uriQuery
        };

        return uriBuilder.Uri.AbsoluteUri;
    }
}
