using Moedelo.CommonV2.UserContext;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.Finances.Business.Models.UserContext
{
    [InjectPerWebRequest]
    public class UserContext : UserContextBaseWithCache
    {
        public UserContext(IUserContextBaseWithCacheDependencies deps) : base(deps)
        {
        }
    }
}