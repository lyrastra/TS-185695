using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.UserAccessControl;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.AccountV2.Client.UserAccessControl;

public interface IUserAccessControlV2ApiClient
{
    /// <summary>Возвращает права пользователя @userId в фирме @firmId</summary>
    Task<ISet<AccessRule>> GetAccessRulesAsync(int firmId, int userId, CancellationToken cancellationToken = default);
    Task<ISet<AccessRule>> GetExplicitUserRulesAsync(int userId, CancellationToken cancellationToken = default);
    Task<List<UserAccountPermissions>> GetAccountPermissionsAsync(int userId, IReadOnlyCollection<int> userIds, CancellationToken cancellationToken = default);
    Task<bool> IsLostAccountAsync(int userId, CancellationToken cancellationToken = default);
    Task<ISet<AccessRule>> GetForUserAsync(int userId, CancellationToken cancellationToken = default);
    Task<int> GetMainUserIdAsync(int firmId, CancellationToken cancellationToken = default);
}