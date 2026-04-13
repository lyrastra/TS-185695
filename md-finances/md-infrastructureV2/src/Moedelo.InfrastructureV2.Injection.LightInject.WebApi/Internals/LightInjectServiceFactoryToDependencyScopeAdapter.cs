using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using LightInject;

namespace Moedelo.InfrastructureV2.Injection.LightInject.WebApi.Internals;

/// <summary>
/// Задача класса - проксировать запросы на создание сервисов в соответствующую фабрику
/// </summary>
internal class LightInjectServiceFactoryToDependencyScopeAdapter : IDependencyScope
{
    private readonly IServiceFactory serviceFactory;

    public LightInjectServiceFactoryToDependencyScopeAdapter(IServiceFactory serviceFactory)
    {
        this.serviceFactory = serviceFactory;
    }

    public object GetService(Type serviceType) => this.serviceFactory.TryGetInstance(serviceType);

    public IEnumerable<object> GetServices(Type serviceType)
    {
        return this.serviceFactory.GetAllInstances(serviceType);
    }

    public void Dispose()
    {
        // не делаем ничего
    }
}
