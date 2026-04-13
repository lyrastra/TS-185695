using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using LightInject;

namespace Moedelo.InfrastructureV2.Injection.LightInject.Web.Internals;

internal sealed class MoedeloWebObjectActivator : IServiceProvider
{
    private const int MaxUnresolvedTypesDictionaryCapacity = 100000;
    private readonly ConcurrentDictionary<Type, bool> typesCannotBeResolved = new ConcurrentDictionary<Type, bool>();
    private readonly IServiceProvider previousProvider;
    private readonly ServiceContainer container;

    public MoedeloWebObjectActivator(
        WebDiInstaller installer,
        IServiceProvider previousProvider)
    {
        this.container = installer.StatefulContainer;
        this.previousProvider = previousProvider;
    }

    public object GetService(Type serviceType)
    {
        // это самый распространённый кейс и самый быстрый способ инстанцировать объект
        if (typesCannotBeResolved.ContainsKey(serviceType))
        {
            return CreateByDefaultWay(serviceType);
        }

        return CreateByInstaller(serviceType)
               ?? CreateByPreviousProvider(serviceType)
               ?? CreateByDefaultWayAndMarkAsUnresolved(serviceType);
    }

    private object CreateByPreviousProvider(Type serviceType)
    {
        return previousProvider?.GetService(serviceType);
    }

    private object CreateByInstaller(Type serviceType)
    {
        // ReSharper disable once InconsistentlySynchronizedField
        var instance = container.TryGetInstance(serviceType);

        if (instance != null)
        {
            return instance;
        }

        if (ShouldBeAutoRegistered(serviceType))
        {
            lock (serviceType)
            {
                // пока ждали lock, тип serviceType уже был зарегистрирован в container
                instance = container.TryGetInstance(serviceType);

                if (instance != null)
                {
                    return instance;
                }

                container.Register(serviceType, new PerRequestLifeTime());

                return container.GetInstance(serviceType);
            }
        }

        return null;
    }

    private static bool ShouldBeAutoRegistered(Type serviceType)
    {
        return ((typeof(Control).IsAssignableFrom(serviceType) // User controls (.ascx), Master Pages (.master) and custom controls inherit from Control class
                         || typeof(IHttpHandler).IsAssignableFrom(serviceType)) // Generic handlers (.ashx) and also pages (.aspx) implements IHttpHandler
                        && (serviceType.GetConstructor(Type.EmptyTypes) == null)); // Performance for controls (LiteralControl, Labels, ...): When there is parameterless constructor, LightInject is not required
        // Note: WebService (ASMX) не поддерживается, так как RestHandler использует Activator.CreateInstance напрямую, не через WebObjectActivator
    }

    private object CreateByDefaultWayAndMarkAsUnresolved(Type serviceType)
    {
        var result = CreateByDefaultWay(serviceType);

        if (result != null)
        {
            if (typesCannotBeResolved.Count < MaxUnresolvedTypesDictionaryCapacity)
            {
                typesCannotBeResolved.TryAdd(serviceType, true);
            }
        }

        return result;
    }

    private static object CreateByDefaultWay(Type serviceType)
    {
        return Activator.CreateInstance(
            serviceType,
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.CreateInstance,
            null,
            null,
            null);
    }
}
