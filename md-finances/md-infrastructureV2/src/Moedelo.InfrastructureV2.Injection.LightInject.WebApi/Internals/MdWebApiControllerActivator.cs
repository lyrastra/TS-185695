using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Hosting;

namespace Moedelo.InfrastructureV2.Injection.LightInject.WebApi.Internals;

/// <summary>
/// Кастомный активатор контроллеров.
/// Обеспечивает переиспользование уже созданного per web request скоупа при создании контроллера
/// </summary>
internal class MdWebApiControllerActivator : IHttpControllerActivator
{
    private readonly IHttpControllerActivator defaultActivator;

    internal MdWebApiControllerActivator(IHttpControllerActivator defaultActivator)
    {
        this.defaultActivator = defaultActivator;
    }

    public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
    {
        if (request.Properties.ContainsKey(HttpPropertyKeys.DependencyScope) == false)
        {
            var scope = request.GetHttContext()?.GetMdDependencyScope();

            if (scope != null)
            {
                var webApiScope = new LightInjectServiceFactoryToDependencyScopeAdapter(scope);
                request.Properties[HttpPropertyKeys.DependencyScope] = webApiScope;
                request.RegisterForDispose(webApiScope);
            }
        }

        return defaultActivator.Create(request, controllerDescriptor, controllerType);
    }
}
