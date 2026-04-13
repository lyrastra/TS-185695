using System.Net.Http;
using System.Text;
using Moedelo.Infrastructure.Consul.Models;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Infrastructure.Consul.Extensions;

internal static class HttpStringContentExtensions
{
    internal static HttpContentWrapper<StringContent> CreateHttpStringContentWrapper(
        this string contentBody, bool disposeContent)
    {
        return new HttpContentWrapper<StringContent>(
            new StringContent(contentBody, Encoding.UTF8),
            disposeContent);
    }

    internal static HttpContentWrapper<StringContent> CreateHttpJsonStringContentWrapper<TContentBody>(
        this TContentBody contentBody, bool disposeContent)
    {
        return new HttpContentWrapper<StringContent>( 
            new StringContent(contentBody.ToJsonString(
                MdSerializerSettingsEnum.ConvertTimeSpanToGoDuration,
                MdSerializerNullHandling.Ignore), Encoding.UTF8),
            disposeContent);
    }
}
