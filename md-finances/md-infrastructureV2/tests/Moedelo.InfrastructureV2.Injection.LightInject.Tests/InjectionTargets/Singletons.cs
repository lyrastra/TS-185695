using System;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
// ReSharper disable ClassNeverInstantiated.Global

/*
 * Файл с различными вариантами объявления зависимостей для синглтонов
 * 
 */

namespace Moedelo.InfrastructureV2.Injection.LightInject.Tests.InjectionTargets;

internal struct GuidMarker
{
    public Guid Marker { get; }
    
    public GuidMarker()
    {
        Marker = Guid.NewGuid();
    }
}

public interface IExplicitSingleton
{
}

// ReSharper disable once InconsistentNaming
public interface IIDISingleton : IDI
{
}

[InjectAsSingleton(typeof(IExplicitSingleton))]
public class ExplicitSingleton : IExplicitSingleton
{
    internal GuidMarker GuidMarker { get; }
}

[InjectAsSingleton]
public class DirectIdiSingleton : IDI
{
    internal GuidMarker GuidMarker { get; }
}

[InjectAsSingleton(typeof(DirectExplicitSingleton))]
public class DirectExplicitSingleton
{
    internal GuidMarker GuidMarker { get; }
}

[InjectAsSingleton]
// ReSharper disable once InconsistentNaming
public class IDISingleton : IIDISingleton
{
    internal GuidMarker GuidMarker { get; }
}

public interface IExplicitSingleton1
{}

public interface IExplicitSingleton2
{}

/// <summary>
/// Синглетон, для которого явно объявлено несколько типов, для которых он должен быть зарегистрирован в контейнере DI
/// </summary>
[InjectAsSingleton(typeof(IExplicitSingleton1), typeof(IExplicitSingleton2))]
public class MultiExplicitSingleton : IExplicitSingleton1, IExplicitSingleton2
{
    internal GuidMarker GuidMarker { get; }
}

