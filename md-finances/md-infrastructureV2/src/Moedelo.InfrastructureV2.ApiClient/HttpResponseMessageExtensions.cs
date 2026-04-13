using System;
using System.Net;
using System.Net.Http;
using Moedelo.InfrastructureV2.Domain.Exceptions.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.InfrastructureV2.ApiClient
{
    public static class HttpResponseMessageExtensions
    {
        /// <exception cref="HttpRequestResponseStatusException"></exception>
        /// <exception cref="HttpRequestValidationException"></exception>
        public static void EnsureSuccessStatusCodeEx(this HttpResponseMessage response,
            HttpQuerySetting httpQuerySetting)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            
            HttpRequestException err;
            if ((int) response.StatusCode >= 500)
            {
                err = Error(response);
            }
            else if ((int) response.StatusCode >= 400)
            {
                if (httpQuerySetting.DontThrowOn404 && response.StatusCode == HttpStatusCode.NotFound)
                {
                    return;
                }

                err = Error4xx(response);
            }
            else
            {
                err = Error(response);
            }

            response.Dispose();
            throw err;
        }

        private static HttpRequestException Error(HttpResponseMessage response, Exception toInner = null)
        {
            var str = response.Content.ReadAsStringAsync().Result;
            return new HttpRequestResponseStatusException(response.StatusCode,
                $"Http request by method {response.RequestMessage.Method.Method} to \"{response.RequestMessage.RequestUri}\" returns bad status code {(int) response.StatusCode} ({response.ReasonPhrase})",
                toInner, str);
        }

        private static HttpRequestException Error4xx(HttpResponseMessage response)
        {
            if ((int) response.StatusCode == 422)
            {
                var str = response.Content.ReadAsStringAsync().Result;
                return new HttpRequestValidationException(response.StatusCode, str, null, str);
            }

            return Error(response);
        }
    }
}