using System.Web.Http;

namespace Moedelo.CommonV2.Auth.WebApi.Extensions;

public static class HttpConfigurationExtensions
{
    public static HttpConfiguration SetupMoedeloAuthentication(this HttpConfiguration httpConfiguration)
    {
        var authHandler = httpConfiguration.DependencyResolver
            .EnsureGetService<WebApiAuthHandler>();

        httpConfiguration.MessageHandlers.Add(authHandler);
        return httpConfiguration;
    }
}
