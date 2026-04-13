#nullable enable
using System;
using System.Web.Http.Dependencies;

namespace Moedelo.CommonV2.Auth.WebApi.Extensions;

internal static class DependencyResolverExtensions
{
    internal static T EnsureGetService<T>(this IDependencyResolver dependencyResolver) => (T)dependencyResolver.GetService(typeof(T))
        ?? throw new ArgumentNullException($"Unable instantiate service {typeof(T).Name}");
}
