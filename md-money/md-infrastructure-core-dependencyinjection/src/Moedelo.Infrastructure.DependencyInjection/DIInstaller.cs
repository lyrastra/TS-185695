using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

[assembly: InternalsVisibleTo("Moedelo.Infrastructure.DependencyInjection.Tests")]
namespace Moedelo.Infrastructure.DependencyInjection;

// ReSharper disable once InconsistentNaming
internal class DIInstaller
{
    private bool initialized;

    private readonly IServiceCollection services;
    private readonly Dictionary<Type, List<Action<IServiceCollection>>> registerBehaviourActionDictionary;
    private readonly Dictionary<Type, bool> multipleImplementationsPossibleDictionary;

    public DIInstaller(IServiceCollection services)
    {
        this.services = services;
        registerBehaviourActionDictionary = new Dictionary<Type, List<Action<IServiceCollection>>>();
        multipleImplementationsPossibleDictionary = new Dictionary<Type, bool>();
    }

    public void Initialize()
    {
        if (initialized)
        {
            return;
        }

        foreach (var action in registerBehaviourActionDictionary.SelectMany(x => x.Value))
        {
            action(services);
        }

        initialized = true;
    }

    // ReSharper disable once InconsistentNaming
    public void RegisterByDIAttribute(string assemblyPath, string assemblySearchPattern)
    {
        var assemblyByPatternList = AssemblyExtensions.GetAssemblyByPattern(assemblyPath, assemblySearchPattern);
        RegisterByDIAttribute(assemblyByPatternList);
    }

    // ReSharper disable once InconsistentNaming
    public void RegisterByDIAttribute(params Assembly[] assemblyList)
    {
        var injectInfoList = assemblyList
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsDefined(typeof(InjectAttribute), inherit: false))
            .SelectMany(GetInterfaces);

        foreach (var (service, implementation, lifetime, registerFactory) in injectInfoList)
        {
            // Основная регистрация сервиса
            RegisterBehaviour(service, serviceCollection => serviceCollection
                .Add(new ServiceDescriptor(service, implementation, lifetime)));

            // Регистрация фабрики, если запрошена
            if (registerFactory)
            {
                var factoryType = typeof(Func<>).MakeGenericType(service);
                // Фабрика регистрируется как Transient, чтобы захватывать текущий ServiceProvider (root или scoped)
                // Это критично для Scoped сервисов - фабрика должна разрешать из текущего scope
                RegisterBehaviour(factoryType, serviceCollection => serviceCollection
                    .AddTransient(factoryType, sp =>
                    {
                        // Создаем делегат, который захватывает текущий ServiceProvider
                        // При каждом запросе Func<T> создается новый делегат с актуальным sp
                        var getMethod = typeof(ServiceProviderServiceExtensions)
                            .GetMethod(nameof(ServiceProviderServiceExtensions.GetRequiredService), new[] { typeof(IServiceProvider) })!
                            .MakeGenericMethod(service);
                            
                        return Delegate.CreateDelegate(factoryType, sp, getMethod);
                    }));
            }
        }
    }

    private static (Type Service, Type Implementation, ServiceLifetime Lifetime, bool RegisterFactory)[] GetInterfaces(Type type)
    {
        var registrations = Attribute.GetCustomAttributes(type, typeof(InjectAttribute), false)
            .Cast<InjectAttribute>()
            .Select(injectAttribute => (Service: injectAttribute.AbstractType,
                Implementation: type,
                Lifetime: GetLifetime(injectAttribute),
                RegisterFactory: injectAttribute.RegisterFactory))
            .ToArray();

        //Если атрибут один и без явного указания типа абстракции, то ищем его интерфейсы, и регистрируем все
        if (registrations.Length == 1 && registrations[0].Service == null)
        {
            registrations = registrations
                .SelectMany(registration => type.GetInterfaces()
                    .Where(baseType => baseType.Namespace?.StartsWith("Moedelo") == true)
                    .Select(baseType => registration with { Service = baseType }))
                .ToArray();
        }

        return registrations
            .Where(reg => reg.Service != null && reg.Service.IsAssignableFrom(reg.Implementation))
            .ToArray();
    }

    private static ServiceLifetime GetLifetime(InjectAttribute attr)
    {
        return attr.Lifetime switch
        {
            InjectionLifetime.Transient => ServiceLifetime.Transient,
            InjectionLifetime.PerScope => ServiceLifetime.Scoped,
            InjectionLifetime.Singleton => ServiceLifetime.Singleton,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void RegisterBehaviour(Type serviceType, Action<IServiceCollection> registerAction)
    {
        if (CheckMultipleImplementationsPossible(serviceType) == false)
        {
            UnregisterBehaviour(serviceType);
        }
        if (registerBehaviourActionDictionary.ContainsKey(serviceType) == false)
        {
            registerBehaviourActionDictionary.Add(serviceType, []);
        }
        registerBehaviourActionDictionary[serviceType].Add(registerAction);
    }

    private void UnregisterBehaviour(Type serviceType)
    {
        if (registerBehaviourActionDictionary.ContainsKey(serviceType))
        {
            registerBehaviourActionDictionary.Remove(serviceType);
        }
    }

    private bool CheckMultipleImplementationsPossible(Type serviceType)
    {
        if (multipleImplementationsPossibleDictionary.TryGetValue(serviceType, out var possible))
        {
            return possible;
        }

        var result = serviceType.IsDefined(typeof(MultipleImplementationsPossibleAttribute), false);
        multipleImplementationsPossibleDictionary.Add(serviceType, result);

        return result;
    }
}