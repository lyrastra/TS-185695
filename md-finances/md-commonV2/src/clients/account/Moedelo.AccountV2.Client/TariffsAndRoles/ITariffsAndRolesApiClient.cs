using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.TariffsAndRoles;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountV2.Client.TariffsAndRoles
{
    public interface ITariffsAndRolesApiClient : IDI
    {
        Task<TariffsAndRolesDto> GetAllAsync();

        Task InvalidateCacheAsync(int firmId, int userId);
    }
}