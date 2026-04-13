using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.InfrastructureV2.Injection.LightInject.Tests.InjectionTargets;

[InjectPerWebRequest(typeof(IPerWebRequestClass))]
public sealed class PerWebRequestClass : IPerWebRequestClass;