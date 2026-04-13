using System.Net.Http;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.AuditWebApi.Extensions
{
    internal static class AuditSpanExtensions
    {
        private const long MinBodyLengthToSaveAsTag = 128;
        private const int MaxUserAgentLength = 255;
        private const long MaxBodyLength = 32768; // 32Кб

        internal static async Task TryAddRequestAsync(this IAuditSpan span,
            HttpRequestMessage request, bool includeRequestBody,
            bool addClientIp, bool addUserAgent)
        {
            span.AddTag("Request.Method", request.Method.ToString());
            if (request.RequestUri != null)
            {
                span.AddTag("Request.OriginalUri", request.RequestUri?.OriginalString);
            }

            if (addClientIp)
            {
                var clientIpAddress = request.GetClientIp();

                if (clientIpAddress != null)
                {
                    span.AddTag("Request.IP", clientIpAddress);
                }
            }

            if (addUserAgent)
            {
                var userAgent = request.GetUserAgent();

                if (userAgent != null)
                {
                    span.AddTag("Request.UserAgent", userAgent.Length < MaxUserAgentLength
                        ? userAgent : userAgent.Substring(0, MaxUserAgentLength));
                }
            }

            if (includeRequestBody)
            {
                var body = await request.TryGetBodyAsync().ConfigureAwait(false);
                if (body != null)
                {
                    if (body.Length >= MinBodyLengthToSaveAsTag)
                    {
                        span.AddTag("Request.Body.Length", body.Length);
                    }

                    if (body.Length > MaxBodyLength)
                    {
                        span.AddTag("Request.Body.First1kb", body.Substring(0, 1024));
                    }
                    else
                    {
                        span.AddTag("Request.Body", body);
                    }
                }
            }
        }
        
        internal static async Task TryAddResponseAsync(
            this IAuditSpan span,
            HttpResponseMessage response,
            bool includeResponseBody,
            bool treatValidationExceptionAsError)
        {
            var statusCode = (int) response.StatusCode;
            
            if (statusCode >= 400 && statusCode <= 599)
            {
                if (treatValidationExceptionAsError || statusCode != 422)
                {
                    span.SetError();
                }
            }
            
            span.AddTag("Response.StatusCode", response.StatusCode);

            if (includeResponseBody)
            {
                var body = await response.TryGetBodyAsync().ConfigureAwait(false);

                if (body != null)
                {
                    if (body.Length > MaxBodyLength)
                    {
                        span.AddTag("Response.Body.Length", body.Length);
                        span.AddTag("Response.Body.First1kb", body.Substring(0, 1024));
                    }
                    else
                    {
                        span.AddTag("Response.Body", body);
                    }
                }
            }
        }
    }
}