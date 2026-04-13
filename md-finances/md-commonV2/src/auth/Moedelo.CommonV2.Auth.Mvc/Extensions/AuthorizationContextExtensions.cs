using System;
using System.Linq;
using System.Web.Mvc;

namespace Moedelo.CommonV2.Auth.Mvc.Extensions;

internal static class AuthorizationContextExtensions
{
    internal static bool HasAttributeOnController<TAttribute>(this AuthorizationContext filterContext)
        where TAttribute : Attribute
    {
        return filterContext.ActionDescriptor.ControllerDescriptor
            .GetCustomAttributes(typeof(TAttribute), true).Any();
    }

    internal static bool HasAttributeOnAction<TAttribute>(this AuthorizationContext filterContext)
        where TAttribute : Attribute
    {
        return filterContext.ActionDescriptor
            .GetCustomAttributes(typeof(TAttribute),true)
            .Any();
    }
}
