using Microsoft.AspNetCore.Authentication;
using Microsoft.Net.Http.Headers;
using Moedelo.Common.ExecutionContext.Auth.Authentication.Models;
using Moedelo.Common.ExecutionContext.Auth.Authentication.Schemes;
using Moedelo.Common.ExecutionContext.Auth.Authentication.Schemes.Handlers;
using System;

namespace Moedelo.Common.ExecutionContext.Auth.Authentication.Extensions
{
    public static class AuthenticationBuilderExtensions
    {
        internal static AuthenticationBuilder AddMoedeloOAuth(this AuthenticationBuilder builder)
        {
            return builder.AddScheme<MoedeloAuthenticationSchemeOptions, OAuthAuthenticationHandler>(MoedeloAuthenticationSchemes.OAuth,
                options =>
                {
                    options.Scheme = MoedeloAuthenticationSchemes.OAuth;
                    options.HeaderName = HeaderNames.Authorization;
                });
        }

        internal static AuthenticationBuilder AddMoedeloApiKey(this AuthenticationBuilder builder)
        {
            return builder.AddScheme<MoedeloAuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(MoedeloAuthenticationSchemes.ApiKey,
                options =>
                {
                    options.Scheme = MoedeloAuthenticationSchemes.ApiKey;
                    options.HeaderName = AuthenticationConstants.ApiKeyHeader;
                });
        }

        internal static AuthenticationBuilder AddMoedeloCookie(this AuthenticationBuilder builder)
        {
            return builder
            .AddScheme<MoedeloAuthenticationSchemeOptions, CookieAuthenticationHandler>(MoedeloAuthenticationSchemes.Cookie,
                options =>
                {
                    options.Scheme = MoedeloAuthenticationSchemes.Cookie;
                    options.HeaderName = HeaderNames.Cookie;
                });
        }

        internal static AuthenticationBuilder AddMoedeloUnidentified(this AuthenticationBuilder builder)
        {
            return builder
            .AddScheme<MoedeloAuthenticationSchemeOptions, UnidentifiedAuthenticationHandler>(MoedeloAuthenticationSchemes.Unidentified,
                options =>
                {
                    options.Scheme = MoedeloAuthenticationSchemes.Unidentified;
                });
        }

        internal static AuthenticationBuilder AddMoedeloDynamic(this AuthenticationBuilder builder, Action<MoedeloAuthenticationOptions> authenticationOptions = null)
        {
            var options = new MoedeloAuthenticationOptions();
            authenticationOptions?.Invoke(options);

            builder.AddPolicyScheme(MoedeloAuthenticationSchemes.Dynamic, "Moedelo dynamic authentication", x => SchemeSelector(x, options));
            if (options.UseOAuth)
            {
                builder.AddMoedeloOAuth();
            }
            if (options.UseApiKey)
            {
                builder.AddMoedeloApiKey();
            }
            if (options.UseCookie)
            {
                builder.AddMoedeloCookie();
            }
            builder.AddMoedeloUnidentified();
            return builder;
        }

        private static void SchemeSelector(PolicySchemeOptions options, MoedeloAuthenticationOptions authenticationOptions)
        {
            options.ForwardDefaultSelector = context =>
            {
                if (authenticationOptions.UseOAuth &&
                context.Request.Headers.TryGetValue(HeaderNames.Authorization, out var authHeader) &&
                    authHeader.ToString().StartsWith(AuthenticationConstants.BearerPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    return MoedeloAuthenticationSchemes.OAuth;
                }
                if (authenticationOptions.UseApiKey &&
                context.Request.Headers.TryGetValue(AuthenticationConstants.ApiKeyHeader, out var apiKeyHeader))
                {
                    return MoedeloAuthenticationSchemes.ApiKey;
                }
                if (authenticationOptions.UseCookie)
                {
                    var mdAuthCookie = context.Request.Cookies[AuthenticationConstants.MdAuthCookie];
                    if (string.IsNullOrWhiteSpace(mdAuthCookie) == false)
                    {
                        return MoedeloAuthenticationSchemes.Cookie;
                    }
                }
                return MoedeloAuthenticationSchemes.Unidentified;
            };
        }
    }
}
