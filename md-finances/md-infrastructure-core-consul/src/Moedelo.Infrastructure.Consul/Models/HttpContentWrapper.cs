using System;
using System.Net.Http;

namespace Moedelo.Infrastructure.Consul.Models;

internal class HttpContentWrapper<THttpContent> : IDisposable where THttpContent: HttpContent
{
    private readonly bool disposeContent;
        
    public THttpContent HttpContent { get; private set; }

    public HttpContentWrapper(THttpContent httpContent, bool disposeContent)
    {
        this.disposeContent = disposeContent;
        HttpContent = httpContent;
    }

    public void Dispose()
    {
        if (disposeContent)
        {
            HttpContent?.Dispose();
            HttpContent = null;
        }
    }
}
