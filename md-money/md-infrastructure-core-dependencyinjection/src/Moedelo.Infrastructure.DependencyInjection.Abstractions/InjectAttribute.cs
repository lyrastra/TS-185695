using System;

namespace Moedelo.Infrastructure.DependencyInjection.Abstractions
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class InjectAttribute : Attribute
    {
        protected InjectAttribute(InjectionLifetime lifetime, Type abstractType, bool registerFactory = false)
        {
            Lifetime = lifetime;
            AbstractType = abstractType;
            RegisterFactory = registerFactory;
        }

        public InjectionLifetime Lifetime { get; }

        public Type AbstractType { get; }

        public bool RegisterFactory { get; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class InjectAsSingletonAttribute : InjectAttribute
    {
        public InjectAsSingletonAttribute(Type abstractType) : base(InjectionLifetime.Singleton, abstractType)
        {
        }
        
        /// <summary>
        /// Регистрируются все абстракции по цепочкам
        /// </summary>
        public InjectAsSingletonAttribute() : base(InjectionLifetime.Singleton, null)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class InjectAsTransientAttribute : InjectAttribute
    {
        public InjectAsTransientAttribute(Type abstractType, bool registerFactory = false) : base(InjectionLifetime.Transient, abstractType, registerFactory)
        {
        }
        
        /// <summary>
        /// Регистрируются все абстракции по цепочкам
        /// </summary>
        public InjectAsTransientAttribute(bool registerFactory = false) : base(InjectionLifetime.Transient, null, registerFactory)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class InjectPerScopeAttribute : InjectAttribute
    {
        public InjectPerScopeAttribute(Type abstractType, bool registerFactory = false) : base(InjectionLifetime.PerScope, abstractType, registerFactory)
        {
        }
        
        /// <summary>
        /// Регистрируются все абстракции по цепочкам
        /// </summary>
        public InjectPerScopeAttribute(bool registerFactory = false) : base(InjectionLifetime.PerScope, null, registerFactory)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Interface)]
    public class MultipleImplementationsPossibleAttribute : Attribute
    {
    }
}