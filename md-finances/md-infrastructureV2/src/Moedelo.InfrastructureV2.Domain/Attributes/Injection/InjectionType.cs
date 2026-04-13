namespace Moedelo.InfrastructureV2.Domain.Attributes.Injection;

public enum InjectionType
{
    Singleton = 1,

    PerWebrequest = 2,

    Transient = 3
}