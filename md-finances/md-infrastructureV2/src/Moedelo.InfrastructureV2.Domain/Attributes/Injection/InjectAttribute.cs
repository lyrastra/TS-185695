using System;

namespace Moedelo.InfrastructureV2.Domain.Attributes.Injection;

[AttributeUsage(AttributeTargets.Class)]
public class InjectAttribute : Attribute
{
    public InjectAttribute(InjectionType injectionType)
    {
        InjectionType = injectionType;
        AbstractType = null;
    }

    protected InjectAttribute(InjectionType injectionType, Type abstractType)
    {
        InjectionType = injectionType;
        AbstractType = abstractType;
    }

    protected InjectAttribute(InjectionType injectionType, Type[] abstractTypes)
    {
        InjectionType = injectionType;
        AbstractTypes = abstractTypes;
    }

    public InjectionType InjectionType { get; private set; }
        
    public Type AbstractType { get; private set; }

    public Type[] AbstractTypes { get; private set; } = Array.Empty<Type>();
}

[AttributeUsage(AttributeTargets.Class)]
public class InjectAsSingletonAttribute : InjectAttribute
{
    public InjectAsSingletonAttribute() : base(InjectionType.Singleton)
    {
    }

    public InjectAsSingletonAttribute(Type abstractType) : base(InjectionType.Singleton, abstractType)
    {
    }

    public InjectAsSingletonAttribute(params Type[] abstractTypes) : base(InjectionType.Singleton, abstractTypes)
    {
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class InjectAsTransientAttribute : InjectAttribute
{
    public InjectAsTransientAttribute() : base(InjectionType.Transient)
    {
    }

    public InjectAsTransientAttribute(Type abstractType) : base(InjectionType.Transient, abstractType)
    {
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class InjectPerWebRequestAttribute : InjectAttribute
{
    public InjectPerWebRequestAttribute() : base(InjectionType.PerWebrequest)
    {
    }

    public InjectPerWebRequestAttribute(Type abstractType) : base(InjectionType.PerWebrequest, abstractType)
    {
    }
}
