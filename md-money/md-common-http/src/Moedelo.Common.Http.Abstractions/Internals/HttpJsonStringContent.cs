using System.Net.Http;

namespace Moedelo.Common.Http.Abstractions.Internals;

internal struct HttpJsonStringContent
{
    public HttpJsonStringContent(HttpContent httpContent, string body)
    {
        HttpContent = httpContent;
        Body = body;
    }

    public HttpContent HttpContent { get; }
    public string Body { get; }
}