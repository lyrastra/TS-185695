using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.Account;
using Moedelo.AccountV2.Dto.Firm;
using Moedelo.AccountV2.Dto.UserAccessControl;
using Moedelo.AccountV2.Dto.UserFirmData;

namespace Moedelo.AccountV2.Client.UserFirmData
{
    public interface IUserFirmDataApiClient
    {
        Task CreateAsync(int firmId, int userId);

        Task<List<UserFirmDataDto>> GetByFirmIdAsync(int firmId, CancellationToken cancellationToken = default);

        Task<List<UserFirmDataDto>> GetByUserAndFirmIdsAsync(int userId, IReadOnlyCollection<int> firmIds, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<UserFirmDataDto>> GetByFirmIdsAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<UserFirmDataDto>> GetByUserIdsAsync(
            IReadOnlyCollection<int> userIds,
            CancellationToken cancellationToken = default);

        Task<List<FirmNameInfoDto>> GetFirmNameListByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        
        Task<List<FirmNameInfoDto>> FindFirmNameListAsync(
            int userId, string query, int limit);

        Task UpdateDateOfLastLoginAndLoginCountAsync(int userId, int firmId);

        Task AddUserFirmDatasAsync(int firmId, int userId, AccountUserFirmDataDto accountUserFirmData);

        Task SaveRolesForCompanyUserAsync(int firmId, int userId, SaveRolesForUserDto saveRolesForUserDto);

        Task SaveRolesForAttachedUserAsync(int firmId, int userId, SaveRolesForUserDto saveRolesForUserDto);

        Task<int> CountByFirmIdAsync(int firmId, CancellationToken cancellationToken);

        Task<int> CountByUserIdsAsync(int userId, CancellationToken cancellationToken);
    }
}