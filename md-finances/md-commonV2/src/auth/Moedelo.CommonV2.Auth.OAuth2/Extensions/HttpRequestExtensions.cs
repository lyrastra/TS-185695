using System;
using System.Web;
using Moedelo.CommonV2.Auth.OAuth2.Services;
using Moedelo.InfrastructureV2.Domain.Helpers;

namespace Moedelo.CommonV2.Auth.OAuth2.Extensions
{
    internal static class HttpRequestExtensions
    {
        private static readonly string authorizationHeaderValueBeginning = $"{AuthorizationTokenHeaderParam.AuthorizationScheme} ";
        private static readonly int authorizationHeaderValueBeginningLength = authorizationHeaderValueBeginning.Length; 

        internal static string GetOAuthToken(this HttpRequest httpRequest)
        {
            var token = GetOAuthTokenFromCookie(httpRequest)
                ?? GetOAuthTokenFromHeader(httpRequest);

            return token?.Trim();
        }

        internal static int GetFirmId(this HttpRequest httpRequest, int defaultFirmId)
        {
            if (httpRequest?.QueryString.HasKeys() != true)
            {
                return defaultFirmId;
            }

            var companyId = httpRequest.QueryString["_companyId"];

            if (companyId != null && int.TryParse(companyId, out var firmId))
            {
                return firmId;
            }

            return defaultFirmId;
        }

        private static string GetOAuthTokenFromCookie(HttpRequest httpRequest)
        {
            var cookie = httpRequest.Cookies.Get(CookieService.MdAuthCookieName);
            var token = cookie?.Value;

            return string.IsNullOrWhiteSpace(token) ? null : token;
        }

        private static string GetOAuthTokenFromHeader(HttpRequest httpRequest)
        {
            var authorizationHeader = httpRequest.Headers.Get(AuthorizationTokenHeaderParam.AuthorizationHeader);

            if (authorizationHeader?.StartsWith(authorizationHeaderValueBeginning, StringComparison.OrdinalIgnoreCase) != true)
            {
                return null;
            }

            var token = authorizationHeader.Substring(authorizationHeaderValueBeginningLength);

            return string.IsNullOrWhiteSpace(token) ? null : token;
        }
    }
}
