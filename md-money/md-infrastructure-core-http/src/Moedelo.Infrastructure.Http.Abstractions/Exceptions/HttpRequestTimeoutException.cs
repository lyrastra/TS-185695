using System;
using System.Net.Http;

namespace Moedelo.Infrastructure.Http.Abstractions.Exceptions
{
    public class HttpRequestTimeoutException : HttpRequestException
    {
        public HttpRequestTimeoutException(string message = null, Exception inner = null)
            : base(message, inner)
        {
        }

        public HttpRequestTimeoutException(HttpRequestMessage requestMessage, Exception inner = null)
            : base($"Http request by method {requestMessage.Method.Method} to \"{requestMessage.RequestUri}\" has timed-out", inner)
        {
        }

        public HttpRequestTimeoutException(
            HttpRequestMessage requestMessage,
            Exception inner,
            TimeSpan timeout,
            TimeSpan duration)
            : base($"Запрос {requestMessage.Method.Method} \"{requestMessage.RequestUri}\" отменён по таймауту после {duration:g} (заданный таймаут {timeout:g})",
                inner)
        {
        }
    }
}