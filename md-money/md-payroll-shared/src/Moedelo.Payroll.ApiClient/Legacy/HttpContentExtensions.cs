using System.Net.Http;
using System.Text;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    internal static class HttpContentExtensions
    {
        internal static HttpContent ToUtf8JsonContent<TRequest>(this TRequest data) where TRequest : class
        {
            return new StringContent(data.ToJsonString(), Encoding.UTF8, "application/json");
        }
    }
}
