using System.Net.Http;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Infrastructure.Http.Abstractions.Interfaces.Factories
{
    public interface IHttpClientFactory
    {
        HttpClient Create(HttpClientSettings settings);
    }
}