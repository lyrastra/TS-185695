using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.CommonV2.UserContext.Domain.Tests.Stubs;

[InjectPerWebRequest(typeof(IUserContext))]
internal sealed class TestingUserContext : UserContextBaseWithCache
{
    public TestingUserContext(IUserContextBaseWithCacheDependencies deps) : base(deps)
    {
    }
}
