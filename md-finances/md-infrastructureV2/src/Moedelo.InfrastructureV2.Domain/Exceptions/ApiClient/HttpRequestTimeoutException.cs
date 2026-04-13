using System;
using System.Net.Http;

namespace Moedelo.InfrastructureV2.Domain.Exceptions.ApiClient;

public class HttpRequestTimeoutException : HttpRequestException
{
    public HttpRequestTimeoutException(string message = null, Exception inner = null)
        : base(message, inner)
    {
    }
        
    public HttpRequestTimeoutException(Uri requestUri, HttpMethod httpMethod, Exception inner)
        : base($"Http request by method {httpMethod.Method} to \"{requestUri}\" has timed-out", inner)
    {
    }

    [Obsolete("Используй другой конструктор")]
    public HttpRequestTimeoutException(HttpRequestMessage requestMessage, Exception inner = null)
        : base($"Http request by method {requestMessage.Method.Method} to \"{requestMessage.RequestUri}\" has timed-out", inner)
    {
    }
}