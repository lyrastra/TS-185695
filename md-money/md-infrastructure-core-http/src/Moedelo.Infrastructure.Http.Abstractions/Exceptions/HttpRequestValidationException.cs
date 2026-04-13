using System;
using System.Net;

namespace Moedelo.Infrastructure.Http.Abstractions.Exceptions
{
    public class HttpRequestValidationException : HttpRequestResponseStatusException
    {
        public HttpRequestValidationException(HttpStatusCode code, string message = null, Exception inner = null)
            : base(code, message, inner)
        {
        }
    }
}