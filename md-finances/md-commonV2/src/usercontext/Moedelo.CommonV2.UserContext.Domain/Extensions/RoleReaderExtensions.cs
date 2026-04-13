using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.CommonV2.UserContext.Domain.Extensions;

public static class RoleReaderExtensions
{
    public static async Task<int> GetRoleIdByCodeAsync(this IRoleReader roleReader, string roleCode)
    {
        const int invalidRoleId = 0;
            
        var roles = await roleReader.GetAllAsync().ConfigureAwait(false);

        return roles.FirstOrDefault(role => role.RoleCode == roleCode)?.Id ?? invalidRoleId;
    }
}