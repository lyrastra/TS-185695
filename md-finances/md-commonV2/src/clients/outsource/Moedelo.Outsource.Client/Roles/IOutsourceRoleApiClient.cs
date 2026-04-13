using System.Threading.Tasks;
using Moedelo.Outsource.Dto.Roles;

namespace Moedelo.Outsource.Client.Roles;

public interface IOutsourceRoleApiClient
{
    Task<int> InsertAsync(int firmId, int userId, RolePostDto model);
}