#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using LightInject;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Injection.Lightinject.Extensions;
using Moedelo.InfrastructureV2.Injection.LightInject.Internals;

// ReSharper disable InconsistentNaming

namespace Moedelo.InfrastructureV2.Injection.Lightinject;

public abstract class DIInstaller : IDIInstaller, IDisposable
{
    protected readonly string tag;

    private bool isInitialized;
    private bool isDisposed;

    protected readonly ILogger logger;
    protected readonly ServiceContainer statefulContainer = new();
    protected readonly ServiceContainer statelessContainer = new();
    private Dictionary<Type, Action<ServiceContainer>>? statefulRegistrations;
    private Dictionary<Type, Action<ServiceContainer>>? statelessRegistrations;
    private readonly List<Tuple<Type, Type>> decoratorRegistrations = [];
    private readonly List<Action> preDisposeActions = [];

    protected DIInstaller(ILogger logger)
    {
        tag = GetType().Name;
        this.logger = logger;

        InitServiceContainers();
    }

    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }

        isDisposed = true;
        PreDisposeActions();

        statefulContainer.Dispose();
        statelessContainer.Dispose();
    }

    public void Initialize(Action<IDiRegistry>? finalizeRegistration = null)
    {
        if (isInitialized)
        {
            return;
        }

        statefulRegistrations = new Dictionary<Type, Action<ServiceContainer>>();
        statelessRegistrations = new Dictionary<Type, Action<ServiceContainer>>();

        var stopWatch = Stopwatch.StartNew();
        // зарегистрируем логер явным образом
        statelessContainer.RegisterInstance(typeof(ILogger), logger);
        CollectServiceRegistrations(finalizeRegistration);
        RegisterServicesInContainers();
        stopWatch.Stop();

        statelessRegistrations = null;
        statefulRegistrations = null;
        decoratorRegistrations.Clear();

        logger.Debug(tag, "Инициализация подсистемы внедрения зависимостей закончена",
            extraData: new
            {
                Duration = stopWatch.Elapsed.ToString("g"),
                StatelessServicesCount = statelessContainer.AvailableServices.Count(),
                StatefulServicesCount = statefulContainer.AvailableServices.Count(),
            });

        // LogDependencyList();

        statefulContainer.ScopeManagerProvider = CreateScopeManagerProviderForStatefulContainer();
        statelessContainer.ScopeManagerProvider = new PerLogicalCallContextScopeManagerProvider();

        isInitialized = true;
    }

    protected virtual IScopeManagerProvider CreateScopeManagerProviderForStatefulContainer()
    {
        return new PerLogicalCallContextScopeManagerProvider();
    }

    public IDisposable BeginScope()
    {
        return statefulContainer.BeginScope();
    }

    public void RegisterPreDisposeHandler(Action action)
    {
        lock (preDisposeActions)
        {
            preDisposeActions.Add(action ?? throw new ArgumentNullException(nameof(action)));
        }
    }

    private void CollectServiceRegistrations(Action<IDiRegistry>? finalizeRegistration)
    {
        try
        {
            RegisterSingleton<IDIResolver>(_ => this);
            RegisterSingleton<IDIInstaller>(_ => this);
            RegisterBehaviour();
            finalizeRegistration?.Invoke(new DiRegistryProxy(this));
        }
        catch (ReflectionTypeLoadException ex)
        {
            logger.Error(tag, ex.Message, ex);
            foreach (var lex in ex.LoaderExceptions)
            {
                logger.Error(tag, lex.Message, lex);
            }

            throw;
        }
    }

    private void RegisterServicesInContainers()
    {
        foreach (var action in statelessRegistrations ?? [])
        {
            action.Value(statelessContainer);
        }

        foreach (var action in statefulRegistrations ?? [])
        {
            action.Value(statefulContainer);
        }

        foreach (var (serviceType, decoratorType) in decoratorRegistrations)
        {
            statefulContainer.Decorate(serviceType, decoratorType);
        }
    }

    private void PreDisposeActions()
    {
        Action[] actions;

        lock (preDisposeActions)
        {
            actions = preDisposeActions.ToArray();
            preDisposeActions.Clear();
        }

        foreach (var action in actions)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception exception)
            {
                logger.Error(tag, "PreDisposeActions", exception);
            }
        }
    }

    protected abstract void RegisterBehaviour();

    private bool CanGetStatelessInstance(Type serviceType, string serviceName)
    {
        return statelessContainer.CanGetInstance(serviceType, serviceName);
    }

    private object GetStatelessInstance(ServiceRequest request)
    {
        return statelessContainer.GetInstance(request.ServiceType);
    }

    private void InitServiceContainers()
    {
        statefulContainer.RegisterFallback(CanGetStatelessInstance, GetStatelessInstance);
        statelessContainer.RegisterFallback((t, serviceName) =>
        {
            if (t?.FullName?.StartsWith("Moedelo.") == true)
            {
                if (string.IsNullOrEmpty(serviceName))
                {
                    logger.Trace(tag, $"Unknown type for stateless context: {t.FullName}");
                }
                else
                {
                    logger.Trace(tag, $"Unknown type for stateless context: {t.FullName} for property '{serviceName}'");
                }
            }

            return false;
        }, static _ => null);
    }

    protected void RegisterByDIAttribute<TAncestor>(params Assembly[] assemblies)
    {
        var diType = typeof(TAncestor);
        RegisterAssemblyByAttribute(diType, assemblies);
    }

    protected void RegisterByDIAttribute(params Assembly[] assemblies)
    {
        var diType = typeof(IDI);
        RegisterAssemblyByAttribute(diType, assemblies);
    }

    protected void RegisterPerWebRequest<TAncestor>(params Assembly[] assemblies)
    {
        RegisterAssembly<TAncestor>(assemblies, () => new PerScopeLifetime());
    }

    protected void RegisterPerWebRequest(params Assembly[] assemblies)
    {
        RegisterAssembly(assemblies, () => new PerScopeLifetime());
    }

    protected void RegisterPerWebRequest(Type ancestor, params Assembly[] assemblies)
    {
        RegisterAssembly(ancestor, assemblies, () => new PerScopeLifetime());
    }

    protected internal void RegisterPerWebRequest<TAncestor, TDescendant>() where TDescendant : TAncestor
    {
        Register<TAncestor, TDescendant>(new PerScopeLifetime());
    }

    protected void RegisterPerWebRequest(Type serviceType, Type implementationType)
    {
        RegisterBehaviour(serviceType, implementationType, new PerScopeLifetime());
    }

    protected void RegisterPerWebRequest<TService>(Func<IDIInstaller, TService> factory)
    {
        RegisterStatefulBehaviour(typeof(TService),
            container => container.Register(_ => factory(this), new PerScopeLifetime()));
    }

    protected void RegisterSingleton<TAncestor>(params Assembly[] assemblies)
    {
        RegisterAssembly<TAncestor>(assemblies, () => new PerContainerLifetime());
    }

    protected void RegisterSingleton(params Assembly[] assemblies)
    {
        RegisterAssembly(assemblies, () => new PerContainerLifetime());
    }

    protected void RegisterSingleton(Type ancestor, params Assembly[] assemblies)
    {
        RegisterAssembly(ancestor, assemblies, () => new PerContainerLifetime());
    }

    protected internal void RegisterSingleton<TAncestor, TDescendant>() where TDescendant : TAncestor
    {
        Register<TAncestor, TDescendant>(new PerContainerLifetime());
    }

    protected void RegisterSingleton(Type serviceType, Type implementationType)
    {
        RegisterBehaviour(serviceType, implementationType, new PerContainerLifetime());
    }

    protected void RegisterSingleton<TService>(Func<IDIInstaller, TService> factory)
    {
        RegisterStatelessBehaviour(typeof(TService),
            container => container.Register(_ => factory(this), new PerContainerLifetime()));
    }
    
    private static ILifetime? DefaultLifetimeFactory() => null;

    protected void RegisterTransient<TAncestor>(params Assembly[] assemblies)
    {
        RegisterAssembly<TAncestor>(assemblies, DefaultLifetimeFactory);
    }

    protected void RegisterTransient(params Assembly[] assemblies)
    {
        RegisterAssembly(assemblies, DefaultLifetimeFactory);
    }

    protected void RegisterTransient(Type ancestor, params Assembly[] assemblies)
    {
        RegisterAssembly(ancestor, assemblies, DefaultLifetimeFactory);
    }

    protected internal void RegisterTransient<TAncestor, TDescendant>() where TDescendant : TAncestor
    {
        Register<TAncestor, TDescendant>(null);
    }

    protected void RegisterDecorator<TAncestor, TDescendant>() where TDescendant : TAncestor
    {
        var serviceType = typeof(TAncestor);
        var implementationType = typeof(TDescendant);
        
        decoratorRegistrations.Add(new Tuple<Type, Type>(serviceType, implementationType));
    }

    protected void RegisterTransient(Type serviceType, Type implementationType)
    {
        RegisterBehaviour(serviceType, implementationType, null);
    }

    protected void RegisterTransient<TService>(Func<IDIInstaller, TService> factory)
    {
        RegisterStatefulBehaviour(typeof(TService),
            container => container.Register(_ => factory(this), (ILifetime?)null));
        RegisterStatelessBehaviour(typeof(TService),
            container => container.Register(_ => factory(this), (ILifetime?)null));
    }

    private void Register<TAncestor, TDescendant>(ILifetime? lifetime) where TDescendant : TAncestor
    {
        RegisterBehaviour(typeof(TAncestor), typeof(TDescendant), lifetime);
    }

    private void RegisterAssembly<TAncestor>(IEnumerable<Assembly> assemblies, Func<ILifetime?> lifetimeFactory)
    {
        var diType = typeof(TAncestor);
        RegisterAssembly(diType, assemblies, lifetimeFactory);
    }

    private void RegisterAssembly(IEnumerable<Assembly> assemblyList, Func<ILifetime?> lifetimeFactory)
    {
        var diType = typeof(IDI);
        RegisterAssembly(diType, assemblyList, lifetimeFactory);
    }

    private void RegisterAssembly(Type diType, IEnumerable<Assembly> assemblyList, Func<ILifetime?> lifetimeFactory)
    {
        var registerBehaviour = (from assembly in assemblyList
                from type in GetPossibleImplementationTypes(assembly)
                    .Where(diType.IsAssignableFromOrIsAssignableFromGenericType)
            let typeInterfaceList = type.GetInterfaces()
                from typeInterface in typeInterfaceList.Where(ti => ti != diType && diType.IsAssignableFromOrIsAssignableFromGenericType(ti))
                let serviceType = typeInterface.IsGenericType && typeInterface.FullName == null
                    ? typeInterface.GetGenericTypeDefinition()
                    : typeInterface
                select new {Service = (typeInterface.IsInterface && typeInterface.IsGenericType && typeInterface.FullName == null) ? typeInterface.GetGenericTypeDefinition() : typeInterface, Implementation = type,}).ToArray();

        foreach (var behaviour in registerBehaviour)
        {
            RegisterBehaviour(behaviour.Service, behaviour.Implementation, lifetimeFactory());
            RegisterBehaviour(behaviour.Implementation, behaviour.Implementation, lifetimeFactory());
        }
    }

    private void RegisterAssemblyByAttribute(Type v2InjectionMarkerType, IEnumerable<Assembly> assemblyList)
    {
        var injections = assemblyList
            .SelectMany(assembly => CollectInjectionSetups(assembly, v2InjectionMarkerType));

        foreach (var injection in injections)
        {
            RegisterBehaviour(injection.InjectionTargetServices, injection.Implementation, injection.Lifetime);
        }
    }

    private IEnumerable<TypeInjectionSetup> CollectInjectionSetups(Assembly assembly, Type injectionMarkerType)
    {
        var possibleImplementations = GetPossibleImplementationTypes(assembly).ToArray();

        return possibleImplementations
            .Select(implementationType => implementationType.GetV2InjectionInfo(injectionMarkerType)
                ?? implementationType.GetStandardInjectionInfo())
            .Where(injectionInfo => injectionInfo != null)!;
    }

    private IEnumerable<Type> GetPossibleImplementationTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract);
        }
        catch (ReflectionTypeLoadException e)
        {
            logger.Error(tag,
                $"При загрузке сборки {assembly.FullName} произошло исключение. Детали смотри в extraData этого сообщения",
                exception: e, extraData: e.LoaderExceptions);

            var loaderExceptionsDetails = string.Join(Environment.NewLine,
                e.LoaderExceptions.Select((ex, i) => $"  [{i + 1}] {ex?.GetType().Name}: {ex?.Message}"));

            var errorMessage = $"При загрузке сборки {assembly.FullName} произошло исключение ReflectionTypeLoadException.{Environment.NewLine}" +
                               $"LoaderExceptions ({e.LoaderExceptions.Length}):{Environment.NewLine}{loaderExceptionsDetails}";

            throw new InvalidOperationException(errorMessage, e);

        }
        catch (Exception e)
        {
            logger.Error(tag, $"При загрузке сборки {assembly.FullName} произошло исключение",
                exception: e);
            throw;
        }
    }

    private void RegisterBehaviour(IEnumerable<Type> serviceTypes, Type implementationType, ILifetime lifetime)
    {
        foreach (var serviceType in serviceTypes)
        {
            RegisterBehaviour(serviceType, implementationType, lifetime);
        }
    }

    private void RegisterBehaviour(Type serviceType, Type implementationType, ILifetime? lifetime)
    {
        var injectionAttribute = (InjectAttribute)Attribute.GetCustomAttribute(implementationType,
            typeof(InjectAttribute), inherit: false);

        var isTransient = lifetime is null or PerRequestLifeTime && (injectionAttribute == null || injectionAttribute.InjectionType == InjectionType.Transient);
        var isStateful = lifetime is PerScopeLifetime && (injectionAttribute == null || injectionAttribute.InjectionType == InjectionType.PerWebrequest);
        var isStateless = lifetime is PerContainerLifetime && (injectionAttribute == null || injectionAttribute.InjectionType == InjectionType.Singleton);

        if (isStateful || isTransient)
        {
            RegisterStatefulBehaviour(serviceType, container => container.Register(serviceType, implementationType, lifetime));
        }

        if (isStateless || isTransient)
        {
            RegisterStatelessBehaviour(serviceType, container => container.Register(serviceType, implementationType, lifetime));
        }
    }

    private void RegisterStatefulBehaviour(Type serviceType, Action<ServiceContainer> registerAction)
    {
        UnregisterStatefulBehaviour(serviceType);
        statefulRegistrations!.Add(serviceType, registerAction);
    }

    private void UnregisterStatefulBehaviour(Type serviceType)
    {
        if (statefulRegistrations!.ContainsKey(serviceType))
        {
            statefulRegistrations.Remove(serviceType);
        }
    }

    private void RegisterStatelessBehaviour(Type serviceType, Action<ServiceContainer> registerAction)
    {
        UnregisterStatelessBehaviour(serviceType);
        statelessRegistrations!.Add(serviceType, registerAction);
    }

    private void UnregisterStatelessBehaviour(Type serviceType)
    {
        if (statelessRegistrations!.ContainsKey(serviceType))
        {
            statelessRegistrations.Remove(serviceType);
        }
    }

    private void LogDependencyList()
    {
        foreach (var service in statefulContainer.AvailableServices.OrderBy(s => s.ServiceType.FullName))
        {
            logger.Trace(tag, service.ToLoggingMessage());
        }

        foreach (var service in statelessContainer.AvailableServices.OrderBy(s => s.ServiceType.FullName))
        {
            logger.Trace(tag, service.ToLoggingMessage());
        }
    }

    public virtual TR GetInstance<TR>()
    {
        return statefulContainer.GetInstance<TR>();
    }

    public TR GetInstanceStateless<TR>()
    {
        return statelessContainer.GetInstance<TR>();
    }

    internal object GetInstance(Type type)
    {
        return statefulContainer.GetInstance(type);
    }
}
