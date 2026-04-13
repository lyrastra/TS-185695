using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Authorization.Dto;

namespace Moedelo.Authorization.Client.Abstractions;

public interface IRoleApiClient
{
    Task<List<RoleDto>> GetRolesAsync();
}