using System;
using System.Net;

namespace Moedelo.InfrastructureV2.Domain.Exceptions.ApiClient;

public class HttpRequestValidationException : HttpRequestResponseStatusException
{
    public HttpRequestValidationException(HttpStatusCode code, string message = null, Exception inner = null, 
        string content = null)
        : base(code, message, inner, content)
    {
    }
}