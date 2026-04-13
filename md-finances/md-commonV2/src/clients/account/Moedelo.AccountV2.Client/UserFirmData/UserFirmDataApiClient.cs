using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.Account;
using Moedelo.AccountV2.Dto.Firm;
using Moedelo.AccountV2.Dto.UserAccessControl;
using Moedelo.AccountV2.Dto.UserFirmData;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.UserFirmData
{
    [InjectAsSingleton(typeof(IUserFirmDataApiClient))]
    public class UserFirmDataApiClient : BaseApiClient, IUserFirmDataApiClient
    {
        private readonly SettingValue apiEndPoint;

        public UserFirmDataApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager
            )
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("UserFirmDataApiEndpoint");
        }

        public Task CreateAsync(int firmId, int userId)
        {
            return PostAsync($"/V2/Create?firmId={firmId}&userId={userId}");
        }

        public Task<List<UserFirmDataDto>> GetByFirmIdAsync(int firmId, CancellationToken cancellationToken)
        {
            return GetAsync<List<UserFirmDataDto>>("/V2/GetByFirmId", new { firmId }, cancellationToken: cancellationToken);
        }

        public Task<List<UserFirmDataDto>> GetByUserAndFirmIdsAsync(
            int userId, IReadOnlyCollection<int> firmIds, CancellationToken cancellationToken)
        {
            const string url = "/V2/GetByUserIdAndFirmIds"; 

            var request = new UserFirmDataByUserAndFirmsSearchCriteriaDto
            {
                UserId = userId,
                FirmIds = firmIds
            };

            return PostAsync<UserFirmDataByUserAndFirmsSearchCriteriaDto, List<UserFirmDataDto>>(
                url, request, cancellationToken: cancellationToken);
        }

        public Task<IReadOnlyCollection<UserFirmDataDto>> GetByFirmIdsAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken = default)
        {
            firmIds.Validation();

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<UserFirmDataDto>>(
                "/V2/GetByFirmIds", firmIds, cancellationToken: cancellationToken);
        }

        public Task<IReadOnlyCollection<UserFirmDataDto>> GetByUserIdsAsync(
            IReadOnlyCollection<int> userIds,
            CancellationToken cancellationToken = default)
        {
            userIds.Validation();

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<UserFirmDataDto>>(
                "/V2/GetByUserIds", userIds, cancellationToken: cancellationToken);
        }

        public Task<List<FirmNameInfoDto>> GetFirmNameListByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            return GetAsync<List<FirmNameInfoDto>>("/V2/GetFirmNameListByUserId", new { userId }, cancellationToken: cancellationToken);
        }

        public Task<List<FirmNameInfoDto>> FindFirmNameListAsync(int userId, string query, int limit)
        {
            return GetAsync<List<FirmNameInfoDto>>("/V2/FirmName/Search", new {userId, query, limit});
        }

        public Task UpdateDateOfLastLoginAndLoginCountAsync(int userId, int firmId)
        {
            return GetAsync("/UpdateDateOfLastLoginAndLoginCount", new { firmId, userId });
        }

        public Task AddUserFirmDatasAsync(int firmId, int userId, AccountUserFirmDataDto accountUserFirmData)
        {
            return PostAsync<AccountUserFirmDataDto>($"/AddUserFirmDatas?firmId={firmId}&userId={userId}", accountUserFirmData);
        }

        public Task SaveRolesForCompanyUserAsync(int firmId, int userId, SaveRolesForUserDto saveRolesForUserDto)
        {
            return PostAsync<SaveRolesForUserDto>($"/SaveRolesForCompanyUser?firmId={firmId}&userId={userId}", saveRolesForUserDto);
        }

        public Task SaveRolesForAttachedUserAsync(int firmId, int userId, SaveRolesForUserDto saveRolesForUserDto)
        {
            return PostAsync<SaveRolesForUserDto>($"/SaveRolesForAttachedUser?firmId={firmId}&userId={userId}", saveRolesForUserDto);
        }

        public Task<int> CountByFirmIdAsync(int firmId, CancellationToken cancellationToken)
        {
            return GetAsync<int>($"/V2/CountUsersByFirmId?firmId={firmId}", cancellationToken: cancellationToken);
        }

        public Task<int> CountByUserIdsAsync(int userId, CancellationToken cancellationToken)
        {
            return GetAsync<int>($"/V2/CountFirmsByUserId?userId={userId}", cancellationToken: cancellationToken);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}