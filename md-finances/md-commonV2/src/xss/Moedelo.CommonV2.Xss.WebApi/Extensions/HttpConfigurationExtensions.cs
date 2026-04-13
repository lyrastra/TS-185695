using System.Web.Http;

namespace Moedelo.CommonV2.Xss.WebApi.Extensions;

public static class HttpConfigurationExtensions
{
    public static HttpConfiguration SetupMoedeloXssProtection(this HttpConfiguration httpConfiguration,
        XssValidationMode mode)
    {
        var handler = httpConfiguration.DependencyResolver
            .EnsureGetService<QueryXssValidationHandler>()
            .SetMode(mode);
        httpConfiguration.MessageHandlers.Add(handler);
        httpConfiguration.Filters.Add(new XssValidationFilterAttribute(){ Mode = mode });

        return httpConfiguration;
    }
}