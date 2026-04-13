using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Moedelo.Common.Logging.ExtraLog.Abstractions;

namespace Moedelo.Common.Logging.ExtraLog.HttpContext
{
    internal sealed class HttpContextExtraLogFieldsProvider : IExtraLogFieldsProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpContextExtraLogFieldsProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<ExtraLogField> Get()
        {
            var requestUrl = GetRequestUrl(httpContextAccessor?.HttpContext);
            
            if (string.IsNullOrEmpty(requestUrl))
            {
                yield break;
            }

            yield return new ExtraLogField("RequestUrl", requestUrl);
        }
        
        private static string GetRequestUrl(Microsoft.AspNetCore.Http.HttpContext context)
        {
            if (context == null)
            {
                return string.Empty;
            }
            
            HttpRequest request;

            try
            {
                request = context.Request;

                if (request == null || !request.Path.HasValue)
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }

            var queryString = request.QueryString.ToString();

            return !string.IsNullOrEmpty(queryString)
                ? $"{request.Path.Value}{queryString}"
                : request.Path.Value;
        }
    }
}