using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto;
using Moedelo.AccountV2.Dto.Account;
using Moedelo.AccountV2.Dto.Filter;
using Moedelo.AccountV2.Dto.FirmCard;
using Moedelo.AccountV2.Dto.FirmUser;
using Moedelo.AccountV2.Dto.UserAccessControl;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.FirmUser
{
    [InjectAsSingleton]
    public class FirmUsersClient : BaseApiClient, IFirmUsersClient
    {
        private readonly SettingValue apiEndPoint;

        public FirmUsersClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmUsersApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<UsersWithAccessToFirmDto> GetUsersHavingAccessToFirmAsync(int firmId, int userId, int companyId)
        {
            var dataWrapper = await PostAsync<DataWrapper<UsersWithAccessToFirmDto>>($"/GetUsersHavingAccessToFirm?firmId={firmId}&userId={userId}&companyId={companyId}").ConfigureAwait(false);
            return dataWrapper.Data;
        }

        public async Task<List<FirmRoleDto>> GetAllRolesForUsersAsync(int currentFirmId, int currentUserId, IReadOnlyCollection<int> userIds)
        {
            userIds = userIds.AsSet();
            var dataWrapper = await PostAsync<IReadOnlyCollection<int>, DataWrapper<List<FirmRoleDto>>>($"/GetAllRolesForUsers" +
                $"?firmId={currentFirmId}&userId={currentUserId}", userIds).ConfigureAwait(false);
            return dataWrapper.Data;
        }

        public async Task<List<FirmRoleDto>> GetAllRolesForUsersAsync(IReadOnlyCollection<int> userIds)
        {
            userIds = userIds.AsSet();
            var dataWrapper = await PostAsync<IReadOnlyCollection<int>, DataWrapper<List<FirmRoleDto>>>($"/GetAllRolesForUsers", userIds).ConfigureAwait(false);
            return dataWrapper.Data;
        }

        public Task<Result> DeleteRoleAsync(int masterFirmId, int masterUserId, int slaveFirmId, int slaveUserId)
        {
            return PostAsync<Result>($"/DeleteRole?firmId={masterFirmId}&userId={masterUserId}&slaveFirmId={slaveFirmId}&slaveUserId={slaveUserId}");
        }

        public Task<Result> SetRoleAsync(int masterFirmId, int masterUserId, ChangeAccessRequest saveRequest)
        {
            return PostAsync<Result>($"/SetRole?firmId={masterFirmId}&userId={masterUserId}&slaveFirmId={saveRequest.FirmId}&slaveUserId={saveRequest.UserId}&roleId={saveRequest.RoleId}");
        }

        public Task<Result> SetDirectorOutsourceRoleToLegalUserAsync(int firmId, int userId, int changeRoleFirmId)
        {
            return PostAsync<Result>($"/SetDirectorOutsourceRoleToLegalUser?firmId={firmId}&userId={userId}&changeRoleFirmId={changeRoleFirmId}");
        }

        public async Task<UsersWithAccessToAccountDto> GetAccountUsersAsync(int firmId, int userId, FilterRequestDto<UserFilterField> dto)
        {
            var dataWrapper = await PostAsync<FilterRequestDto<UserFilterField>, DataWrapper<UsersWithAccessToAccountDto>>($"/GetAccountUsers" +
                    $"?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return dataWrapper.Data;
        }

        public Task<Result> DepriveUsersOfAdminRightsAsync(int masterFirmId, int masterUserId, IReadOnlyCollection<int> adminIds)
        {
            adminIds = adminIds.AsSet();
            return PostAsync<IReadOnlyCollection<int>, Result >($"/DepriveUsersOfAdminRights?firmId={masterFirmId}&userId={masterUserId}", adminIds);
        }

        public async Task<FirmCardUsersAutocompleteDto> GetFirmCardUsersAutocompleteAsync(int currentFirmId, int currentUserId, string query,
            int firmId, int count)
        {
            var dataWrapper = await PostAsync<object, DataWrapper<FirmCardUsersAutocompleteDto>>($"/GetFirmCardUsersAutocomplete" +
                $"?firmId={currentFirmId}&userId={currentUserId}", new { query, firmId, count }).ConfigureAwait(false);
            return dataWrapper.Data;
        }

        public Task<ListWithTotalCount<FirmUserDto>> GetFirmUsersAsync(int firmId, int userId, FilterRequestDto<UserFilterField> dto)
        {
            return PostAsync<FilterRequestDto<UserFilterField>, ListWithTotalCount<FirmUserDto>>($"/GetFirmUsers?firmId={firmId}&userId={userId}", dto);
        }
    }
}