using System;
using System.Net;
using System.Net.Http;

namespace Moedelo.Infrastructure.Http.Abstractions.Exceptions
{
    public class HttpRequestResponseStatusException : HttpRequestException
    {
        public HttpRequestResponseStatusException(
            HttpStatusCode code, 
            string message = null, 
            Exception inner = null,
            string content = null)
            : base(message, inner)
        {
            StatusCode = code;
            Content = content;
        }

        public HttpStatusCode StatusCode { get; }
        public int StatusCodeInt => (int) StatusCode;

        public string Content { get; }
    }
}