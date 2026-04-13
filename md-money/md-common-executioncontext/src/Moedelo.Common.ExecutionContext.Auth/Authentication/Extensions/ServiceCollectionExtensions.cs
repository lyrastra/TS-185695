using Microsoft.Extensions.DependencyInjection;
using Moedelo.Common.ExecutionContext.Auth.Authentication.Models;
using Moedelo.Common.ExecutionContext.Auth.Authentication.Schemes;
using System;

namespace Moedelo.Common.ExecutionContext.Auth.Authentication.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMoedeloAuthentication(this IServiceCollection services, Action<MoedeloAuthenticationOptions> authenticationOptions = null)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = MoedeloAuthenticationSchemes.Dynamic;
                options.DefaultChallengeScheme = MoedeloAuthenticationSchemes.Dynamic;
            }).AddMoedeloDynamic(authenticationOptions);
            return services;
        }

        public static IServiceCollection AddMoedeloOAuthAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = MoedeloAuthenticationSchemes.OAuth;
                options.DefaultChallengeScheme = MoedeloAuthenticationSchemes.OAuth;
            }).AddMoedeloOAuth();
            return services;
        }

        public static IServiceCollection AddMoedeloApiKeyAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = MoedeloAuthenticationSchemes.ApiKey;
                options.DefaultChallengeScheme = MoedeloAuthenticationSchemes.ApiKey;
            }).AddMoedeloApiKey();
            return services;
        }

        public static IServiceCollection AddMoedeloCookieAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = MoedeloAuthenticationSchemes.Cookie;
                options.DefaultChallengeScheme = MoedeloAuthenticationSchemes.Cookie;
            }).AddMoedeloCookie();
            return services;
        }

        public static IServiceCollection AddMoedeloUnidentifiedAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = MoedeloAuthenticationSchemes.Unidentified;
                options.DefaultChallengeScheme = MoedeloAuthenticationSchemes.Unidentified;
            }).AddMoedeloUnidentified();
            return services;
        }
    }
}
