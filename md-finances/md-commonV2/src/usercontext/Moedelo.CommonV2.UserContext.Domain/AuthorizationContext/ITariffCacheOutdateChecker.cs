using System.Threading.Tasks;

namespace Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;

public interface ITariffCacheOutdateChecker
{
    Task<bool> IsOutdatedAsync();

    void OnCacheRefreshed();
}