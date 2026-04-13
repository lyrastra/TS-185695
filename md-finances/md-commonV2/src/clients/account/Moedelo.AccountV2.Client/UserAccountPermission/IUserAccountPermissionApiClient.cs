using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.UserAccountPermission;

namespace Moedelo.AccountV2.Client.UserAccountPermission;

public interface IUserAccountPermissionApiClient
{
    /// <summary>
    /// Удаление прав на акаунт
    /// </summary>
    Task DeleteAsync(IReadOnlyCollection<int> userIds);

    /// <summary>
    /// Проверка принадлежности админских прав
    /// </summary>
    Task<bool> IsAdminAsync(int userId, CancellationToken cancellationToken = default);

    Task SaveAsync(int firmId, int userId, IReadOnlyCollection<UserAccountPermissionDto> dto);
}