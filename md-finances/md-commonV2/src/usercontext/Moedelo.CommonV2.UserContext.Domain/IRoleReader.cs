using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;

namespace Moedelo.CommonV2.UserContext.Domain;

public interface IRoleReader
{
    Task<RoleInfo> GetAsync(int roleId);
    Task<IReadOnlyCollection<RoleInfo>> GetAllAsync();
}