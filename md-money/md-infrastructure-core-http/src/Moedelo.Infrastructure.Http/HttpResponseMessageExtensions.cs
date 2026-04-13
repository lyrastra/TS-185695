using System;
using System.Net;
using System.Net.Http;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Infrastructure.Http
{
    public static class HttpResponseMessageExtensions
    {
        /// <exception cref="HttpRequestResponseStatusException"></exception>
        /// <exception cref="HttpRequestValidationException"></exception>
        public static void EnsureSuccessStatusCodeEx(
            this HttpResponseMessage response,
            HttpQuerySetting querySettings,
            string content = null)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            HttpRequestException err;
            if ((int) response.StatusCode >= 500)
            {
                err = Error(response, content: content);
            }
            else if ((int) response.StatusCode >= 400)
            {
                if (querySettings.DontThrowOn404 && response.StatusCode == HttpStatusCode.NotFound)
                {
                    return;
                }
                
                err = Error4xx(response, content: content);
            }
            else
            {
                err = Error(response, content: content);
            }

            response.Content?.Dispose();
            throw err;
        }

        private static HttpRequestException Error(HttpResponseMessage response, Exception toInner = null, string content = null)
        {
            return new HttpRequestResponseStatusException(response.StatusCode,
                $"Http request by method {response.RequestMessage.Method.Method} to \"{response.RequestMessage.RequestUri}\" returns bad status code {(int) response.StatusCode} ({response.ReasonPhrase})",
                toInner,
                content: content);
        }

        private static HttpRequestException Error4xx(HttpResponseMessage response, string content = null)
        {
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity) // 422
            {
                // тело запроса при 422 должно начитываться в любом случае, поддержки старого кода ради
                content ??= response.Content.ReadAsStringAsync().Result;
                return new HttpRequestValidationException(response.StatusCode, content);
            }

            return Error(response, content: content);
        }
    }
}