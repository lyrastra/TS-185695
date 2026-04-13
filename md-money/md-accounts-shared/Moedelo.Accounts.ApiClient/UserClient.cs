using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Dto.Users;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Accounts.Clients.Extensions;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounts.Clients
{
    [InjectAsSingleton(typeof(IUserClient))]
    internal sealed class UserClient : BaseLegacyApiClient, IUserClient
    {
        public UserClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<UserClient> logger)
            : base(
                    httpRequestExecuter,
                    uriCreator,
                    auditTracer,
                    auditHeadersGetter,
                    settingRepository.Get("UserApiEndpoint"),
                    logger)
        {
        }

        public Task<BaseUserInfoDto> GetOwnerByCurrentFirmId(int firmId)
        {
            return GetAsync<BaseUserInfoDto>($"/GetOwnerByCurrentFirmId?firmId={firmId}");
        }

        public Task<int?> GetFirmIdByLoginAsync(string login)
        {
            return GetAsync<int?>($"/V2/GetFirmIdByLogin?login={login}");
        }

        public Task<BaseUserInfoDto> GetUserInfoByIdAsync(int userId)
        {
            return GetAsync<BaseUserInfoDto>($"/GetBaseUserInfo?userId={userId}");
        }
        
        public Task<List<BaseUserInfoDto>> GetBaseUserInfoListByLoginsAsync(IReadOnlyCollection<string> logins)
        {
            logins = new HashSet<string>(logins);
            return PostAsync<IReadOnlyCollection<string>, List<BaseUserInfoDto>>("/GetBaseUserInfoListByLogins", logins);
        }

        public Task<UserDto[]> GetByIdsAsync(IReadOnlyCollection<int> userIds)
        {
            return PostAsync<IReadOnlyCollection<int>, UserDto[]>("/V2/GetByIds", userIds);
        }

        public Task<IReadOnlyDictionary<int, int?>> GetAdditionalRegionIdsByUserIdsAsync(IReadOnlyCollection<int> ids)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, int?>>(
                "/V2/GetAdditionalRegionIdsByUserIds",
                ids);
        }

        public Task<List<BaseUserInfoDto>> GetUserInfoListByIdAsync(IEnumerable<int> userIds)
        {
            return PostAsync<IEnumerable<int>, List<BaseUserInfoDto>>("/GetBaseUserInfoList", userIds);
        }

        public Task<List<BaseUserInfoDto>> GetUserInfoListByFirmIdAsync(IEnumerable<int> firmIds)
        {
            return PostAsync<IEnumerable<int>, List<BaseUserInfoDto>>("/V2/GetBaseUserInfoListByFirmIds", firmIds);
        }

        public Task<List<FirmLoginDto>> GetUserLoginsByFirmIdsAsync(IEnumerable<int> firmIds)
        {
            return PostAsync<IEnumerable<int>, List<FirmLoginDto>>("/V2/GetUserLoginsByFirmIds", firmIds);
        }

        public Task<UtmFieldsDto> GetUtmFieldsByUserIdAsync(int userId)
        {
            return GetAsync<UtmFieldsDto>($"/V2/UtmFields?userId={userId}");
        }

        public Task<IReadOnlyDictionary<int, UtmFieldsDto>> GetUtmFieldsByUserIdsAsync(
            IReadOnlyCollection<int> userIds,
            CancellationToken cancellationToken = default)
        {
            userIds.Validation();

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, UtmFieldsDto>>(
                "/V2/GetUtmFieldsByUserIds",
                userIds,
                cancellationToken: cancellationToken);
        }

        public Task<List<UserDto>> GetUsersByLoginAutocompleteAsync(string loginPart, int count)
        {
            if (count <= 0 || count > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return GetAsync<List<UserDto>>("/V2/GetUsersByLoginAutocomplete", new { loginPart, count });
        }

        public async Task<bool> IsDeletedAsync(int userId)
        {
            var response = await GetAsync<DataResponseWrapper<bool>>($"/IsDeleted?userId={userId}");
            return response.Data;
        }

        public Task<int> CreateAsync(UserDto user)
        {
            return PostAsync<UserDto, int>("/V2/Create", user);
        }

        public async Task<bool> CanUserAccessToFirmAsync(UserId userId, FirmId firmId)
        {
            var response = await GetAsync<DataResponseWrapper<bool>>($"/CanUserAccessToFirm?userId={(int)userId}&firmId={(int)firmId}");
            return response.Data;
        }

        public Task<IReadOnlyCollection<UserDto>> GetByAccountIdsAsync(
            IReadOnlyCollection<int> accountIds,
            CancellationToken cancellationToken = default)
        {
            accountIds.Validation();

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<UserDto>>(
                "/V2/GetByAccountIds", accountIds, cancellationToken: cancellationToken);
        }

        public Task<IReadOnlyDictionary<int, bool>> GetFlagsIsDeletedForUserIdsAsync(
            IReadOnlyCollection<int> userIds,
            CancellationToken cancellationToken = default)
        {
            userIds.Validation();

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, bool>>(
                "/V2/GetFlagsIsDeletedForUserIds", userIds, cancellationToken: cancellationToken);
        }

        public Task<IReadOnlyCollection<int>> GetSourceIdsByTargetUserIdsAsync(
           IReadOnlyCollection<int> targetUserIds,
           CancellationToken cancellationToken = default)
        {
            targetUserIds.Validation();

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<int>>(
                "/V2/GetSourceIdsByTargetUserIds", targetUserIds, cancellationToken: cancellationToken);
        }
    }
}
