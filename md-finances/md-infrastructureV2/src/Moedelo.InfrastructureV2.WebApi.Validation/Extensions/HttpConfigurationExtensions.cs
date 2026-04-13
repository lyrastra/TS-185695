#nullable enable
using System.Web.Http;
using Moedelo.InfrastructureV2.WebApi.Validation.Filters;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Extensions;

public static class HttpConfigurationExtensions
{
    public static HttpConfiguration SetupValidationFilter(this HttpConfiguration httpConfiguration,
        bool sendExceptionsInfo)
    {
        httpConfiguration.Filters.Add(new WebApiValidateModelStateFilterAttribute(sendExceptionsInfo));

        return httpConfiguration;
    }
}
