using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto;
using Moedelo.AccountV2.Dto.Account;
using Moedelo.AccountV2.Dto.Filter;
using Moedelo.AccountV2.Dto.FirmCard;
using Moedelo.AccountV2.Dto.FirmUser;
using Moedelo.AccountV2.Dto.UserAccessControl;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountV2.Client.FirmUser
{
    public interface IFirmUsersClient : IDI
    {
        Task<UsersWithAccessToFirmDto> GetUsersHavingAccessToFirmAsync(int firmId, int userId, int companyId);
        Task<List<FirmRoleDto>> GetAllRolesForUsersAsync(int currentFirmId, int currentUserId, IReadOnlyCollection<int> userIds);
        Task<List<FirmRoleDto>> GetAllRolesForUsersAsync(IReadOnlyCollection<int> userIds);
        Task<Result> DeleteRoleAsync(int masterFirmId, int masterUserId, int slaveFirmId, int slaveUserId);
        Task<UsersWithAccessToAccountDto> GetAccountUsersAsync(int firmId, int userId, FilterRequestDto<UserFilterField> dto);
        Task<Result> SetRoleAsync(int masterFirmId, int masterUserId, ChangeAccessRequest saveRequest);
        Task<Result> SetDirectorOutsourceRoleToLegalUserAsync(int firmId, int userId, int changeRoleFirmId);
        Task<Result> DepriveUsersOfAdminRightsAsync(int masterFirmId, int masterUserId, IReadOnlyCollection<int> adminIds);
        Task<FirmCardUsersAutocompleteDto> GetFirmCardUsersAutocompleteAsync(int currentFirmId, int currentUserId, string query, int firmId, int count);
        Task<ListWithTotalCount<FirmUserDto>> GetFirmUsersAsync(int firmId, int userId, FilterRequestDto<UserFilterField> dto);
    }
}