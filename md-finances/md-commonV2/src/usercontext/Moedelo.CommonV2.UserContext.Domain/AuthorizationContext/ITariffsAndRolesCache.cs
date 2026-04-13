using System.Threading.Tasks;

namespace Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;

public interface ITariffsAndRolesCache
{
    Task<TariffsAndRoles> GetTariffsAndRolesAsync();

    void Invalidate();
}