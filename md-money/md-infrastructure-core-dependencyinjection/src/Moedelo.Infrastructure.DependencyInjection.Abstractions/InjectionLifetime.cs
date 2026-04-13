namespace Moedelo.Infrastructure.DependencyInjection.Abstractions
{
    public enum InjectionLifetime
    {
        Singleton = 1,

        PerScope = 2,

        Transient = 3,
    }
}