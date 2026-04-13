#nullable enable
using System.Web;
using LightInject;
using Moedelo.InfrastructureV2.Injection.LightInject.Web;

namespace Moedelo.InfrastructureV2.Injection.LightInject.WebApi.Internals;

internal static class HttpContextBaseExtensions
{
    internal static Scope? GetMdDependencyScope(this HttpContextBase httpContext)
    {
        if (httpContext.Items.Contains(HttpContextItemNames.MdScopeKey))
        {
            return httpContext.Items[HttpContextItemNames.MdScopeKey] as Scope;
        }

        return null;
    }
}
