using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Dto.Users;
using Moedelo.Common.Types;

namespace Moedelo.Accounts.Abstractions.Interfaces
{
    public interface IUserClient
    {
        Task<BaseUserInfoDto> GetOwnerByCurrentFirmId(int firmId);

        Task<int?> GetFirmIdByLoginAsync(string login);

        Task<BaseUserInfoDto> GetUserInfoByIdAsync(int userId);

        Task<List<BaseUserInfoDto>> GetBaseUserInfoListByLoginsAsync(IReadOnlyCollection<string> logins);

        Task<UserDto[]> GetByIdsAsync(IReadOnlyCollection<int> userIds);

        Task<List<BaseUserInfoDto>> GetUserInfoListByIdAsync(IEnumerable<int> userIds);

        Task<List<BaseUserInfoDto>> GetUserInfoListByFirmIdAsync(IEnumerable<int> firmIds);
        Task<List<FirmLoginDto>> GetUserLoginsByFirmIdsAsync(IEnumerable<int> firmIds);
        Task<UtmFieldsDto> GetUtmFieldsByUserIdAsync(int userId);

        Task<IReadOnlyDictionary<int, UtmFieldsDto>> GetUtmFieldsByUserIdsAsync(
            IReadOnlyCollection<int> userIds,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает список пользователей, начинающихся на loginPart
        /// </summary>
        /// <param name="count">Значение не больше 10</param>
        Task<List<UserDto>> GetUsersByLoginAutocompleteAsync(string loginPart, int count);

        Task<bool> IsDeletedAsync(int userId);

        Task<int> CreateAsync(UserDto user);

        Task<bool> CanUserAccessToFirmAsync(UserId userId, FirmId firmId);

        Task<IReadOnlyDictionary<int, int?>> GetAdditionalRegionIdsByUserIdsAsync(IReadOnlyCollection<int> userIds);

        Task<IReadOnlyCollection<UserDto>> GetByAccountIdsAsync(
             IReadOnlyCollection<int> accountIds,
             CancellationToken cancellationToken = default);

        Task<IReadOnlyDictionary<int, bool>> GetFlagsIsDeletedForUserIdsAsync(
            IReadOnlyCollection<int> userIds,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<int>> GetSourceIdsByTargetUserIdsAsync(
            IReadOnlyCollection<int> targetUserIds,
            CancellationToken cancellationToken = default);
    }
}
