using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.Requisites;
using Moedelo.AccountV2.Dto.User;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.User
{
    [InjectAsSingleton(typeof(IUserApiClient))]
    public class UserApiClient : BaseApiClient, IUserApiClient
    {
        private readonly SettingValue apiEndPoint;

        public UserApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.GetRequired("UserApiEndpoint");
        }

        public Task<BaseUserInfoDto> GetBaseUserInfoAsync(int userId, CancellationToken cancellationToken)
        {
            return GetAsync<BaseUserInfoDto>("/GetBaseUserInfo", new { userId }, cancellationToken: cancellationToken);
        }

        public Task<UserLoginDto> GetUserLoginAsync(int userId)
        {
            return GetAsync<UserLoginDto>("/v2/Login", new { userId });
        }

        public Task<List<BaseUserInfoDto>> GetBaseUserInfoListAsync(IReadOnlyCollection<int> userIds)
        {
            userIds = userIds.AsSet();
            return PostAsync<IReadOnlyCollection<int>, List<BaseUserInfoDto>>("/GetBaseUserInfoList", userIds);
        }

        public Task<List<BaseUserInfoDto>> GetBaseUserInfoListByLoginsAsync(IReadOnlyCollection<string> logins)
        {
            logins = logins.AsSet();
            return PostAsync<IReadOnlyCollection<string>, List<BaseUserInfoDto>>("/GetBaseUserInfoListByLogins", logins);
        }

        public async Task<BaseUserInfoDto> GetBaseUserInfoByFirmIdAsync(int firmId)
        {
            var listDto = await GetBaseUserInfoListByFirmIdsAsync(new[] {firmId}).ConfigureAwait(false);

            return listDto.FirstOrDefault();
        }

        public Task<List<BaseUserInfoDto>> GetBaseUserInfoListByFirmIdsAsync(IReadOnlyCollection<int> firmIds)
        {
            firmIds = firmIds.AsSet();
            return PostAsync<IReadOnlyCollection<int>, List<BaseUserInfoDto>>("/V2/GetBaseUserInfoListByFirmIds", firmIds);
        }

        public Task<List<FirmLoginDto>> GetUserLoginsByFirmIdsAsync(IReadOnlyCollection<int> firmIds)
        {
            firmIds = firmIds.AsSet();
            return PostAsync<IReadOnlyCollection<int>, List<FirmLoginDto>>("/V2/GetUserLoginsByFirmIds", firmIds);
        }
        
        public Task<AdditionalUserInfoDto> GetAdditionalUserInfoAsync(int userId, CancellationToken cancellationToken)
        {
            const string uri = "/GetAdditionalUserInfo";

            return GetAsync<AdditionalUserInfoDto>(uri, new {userId}, cancellationToken: cancellationToken);
        }

        public Task<List<UserIdFirmIdResponseDto>> GetAllFirmUserIdsAndFirmIdsByProductPlatformAsync(string productPlatform, bool onlyPaid)
        {
            return GetAsync<List<UserIdFirmIdResponseDto>>("/GetAllFirmUserIdsAndFirmIdsByProductPlatform", new {productPlatform, onlyPaid});
        }

        public Task<List<UserIdFirmIdResponseDto>> GetAllFirmUserIdsAndFirmIdsByUserIdsAsync(IReadOnlyCollection<int> userIds)
        {
            userIds = userIds.AsSet();
            return PostAsync<IReadOnlyCollection<int>, List<UserIdFirmIdResponseDto>>("/GetAllFirmUserIdsAndFirmIdsByUserIds", userIds);
        }

        public Task<List<UserDto>> GetByIdsAsync(IReadOnlyCollection<int> userIds)
        {
            userIds = userIds.AsSet();
            
            if (!userIds.Any())
            {
                return Task.FromResult(new List<UserDto>());
            }
            
            return PostAsync<IReadOnlyCollection<int>, List<UserDto>>("/V2/GetByIds", userIds);
        }

        public Task<IReadOnlyCollection<UserDto>> GetByAccountIdsAsync(
            IReadOnlyCollection<int> accountIds,
            CancellationToken cancellationToken = default)
        {
            accountIds.Validation();

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<UserDto>>(
                "/V2/GetByAccountIds", accountIds, cancellationToken: cancellationToken);
        }

        public Task<UserDto> GetByLoginAsync(string login, CancellationToken cancellationToken)
        {
            return GetAsync<UserDto>("/V2/GetByLogin", new { login }, cancellationToken: cancellationToken);
        }
        
        public Task<List<UserDto>> GetUsersByLoginAutocompleteAsync(string loginPart, int count)
        {
            if (count is <= 0 or > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Ожидается значение от 1 до 10");
            }
            
            return GetAsync<List<UserDto>>("/V2/GetUsersByLoginAutocomplete", new { loginPart, count });
        }

        public Task<int?> GetFirmIdByLoginAsync(string login)
        {
            return GetAsync<int?>("/V2/GetFirmIdByLogin", new { login });
        }

        public Task<CreateUserPartnerResponseDto> CreateSberPartnerAsync(UserPartnerDto user)
        {
            return PostAsync<UserPartnerDto, CreateUserPartnerResponseDto>("/V2/CreateSberPartner", user);
        }

        public Task<int> CreateAsync(UserDto user)
        {
            return PostAsync<UserDto, int>("/V2/Create", user);
        }

        public async Task<bool> CheckPasswordAsync(int userId, string password)
        {
            var response = await GetAsync<DataWrapper<bool>>("/CheckPassword", new { userId, password }).ConfigureAwait(false);
            return response.Data;
        }

        public Task<LoginChangeResponseDto> ChangeLoginWithoutConfirmationAsync(
            LoginChangeRequestDto requestDto, CancellationToken cancellationToken)
        {
            const string uri = "/V2/login/change/withoutConfirmation";

            return PostAsync<LoginChangeRequestDto, LoginChangeResponseDto>(uri, requestDto,
                cancellationToken: cancellationToken);
        }

        public Task<ConfirmUserLoginChangingResponseDto> ConfirmLoginChangingByCodeAsync(
            ConfirmUserLoginChangingDto requestDto, CancellationToken cancellationToken)
        {
            const string uri = "/V2/login/change/confirm";

            return PostAsync<ConfirmUserLoginChangingDto, ConfirmUserLoginChangingResponseDto>(uri, requestDto,
                cancellationToken: cancellationToken);
        }

        public Task<ConfirmUserLoginChangingResponseDto> ConfirmLoginChangingByCodeAsync(
            ConfirmUserFirstLoginChangingDto requestDto, CancellationToken cancellationToken)
        {
            const string uri = "/V2/login/first-change/confirm";

            return PostAsync<ConfirmUserFirstLoginChangingDto, ConfirmUserLoginChangingResponseDto>(uri, requestDto,
                cancellationToken: cancellationToken);
        }

        public Task<LoginChangeWithEmailConfirmationResponseDto> RequestLoginChangingAsync(
            LoginChangeWithEmailConfirmationRequestDto requestDto, CancellationToken cancellationToken)
        {
            const string uri = "/V2/login/change/request";
            
            return PostAsync<LoginChangeWithEmailConfirmationRequestDto, LoginChangeWithEmailConfirmationResponseDto>(uri,
                requestDto, cancellationToken: cancellationToken);
        }

        public Task<FirstLoginChangeWithEmailConfirmationResponseDto> RequestLoginChangingAsync(
            FirstLoginChangeWithEmailConfirmationRequestDto requestDto, CancellationToken cancellationToken)
        {
            const string uri = "/V2/login/first-change/request";

            return PostAsync<FirstLoginChangeWithEmailConfirmationRequestDto, FirstLoginChangeWithEmailConfirmationResponseDto>(uri,
                requestDto, cancellationToken: cancellationToken);
        }

        public Task<BaseUserResponseDto> PasswordRecoveryAsync(PasswordRecoveryRequestDto requestDto)
        {
            return PostAsync<PasswordRecoveryRequestDto, BaseUserResponseDto>("/V2/PasswordRecovery", requestDto);
        }

        public Task<GetUtmFieldsV2Dto> GetUtmFieldsByUserIdAsync(int userId)
        {
            return GetAsync<GetUtmFieldsV2Dto>("/V2/UtmFields", new {userId});
        }

        public async Task<bool> CanUserAccessToFirmAsync(int firmId, int userId)
        {
            var data = await GetAsync<DataWrapper<bool>>("/CanUserAccessToFirm", new { userId, firmId }).ConfigureAwait(false);
            return data.Data;
        }

        public async Task<int> GetDefaultFirmIdAsync(int userId, CancellationToken cancellationToken)
        {
            var data = await GetAsync<DataWrapper<int>>("/GetDefaultFirmId", new { userId }, cancellationToken: cancellationToken).ConfigureAwait(false);
            return data.Data;
        }

        public Task<int> GetDefaultFirmIdOrZeroAsync(int userId, CancellationToken cancellationToken)
        {
            return GetAsync<int>("/v2/DefaultFirmIdOrZero", new { userId }, cancellationToken: cancellationToken);
        }

        public Task<AdminRequisitesDto> GetAdminRequisitesAsync(int userId)
        {
            return GetAsync<AdminRequisitesDto>("/GetAdminRequisites", new { userId });
        }

        public Task<BaseUserInfoDto> GetOwnerByCurrentFirmIdAsync(int firmId,
            CancellationToken cancellationToken = default)
        {
            return GetAsync<BaseUserInfoDto>("/GetOwnerByCurrentFirmId", new { firmId }, cancellationToken: cancellationToken);
        }

        public Task SetUtmFieldsAsync(SetUtmFieldsDto dto)
        {
            return PostAsync("/SetUtmFieldsAsync", dto);
        }

        public async Task<bool> IsDeletedAsync(int userId)
        {
            var result = await GetAsync<DataWrapper<bool>>("/IsDeleted", new {userId}).ConfigureAwait(false);
            return result.Data;
        }

        public Task<IReadOnlyDictionary<int, bool>> GetFlagsIsDeletedForUserIdsAsync(
            IReadOnlyCollection<int> userIds,
            CancellationToken cancellationToken = default)
        {
            userIds.Validation();

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, bool>>(
                "/V2/GetFlagsIsDeletedForUserIds", userIds, cancellationToken: cancellationToken);
        }

        public Task<bool> MarkAsDeletedAsync(int userId)
        {
            return PostAsync<bool>($"/v2/MarkAsDeleted/{userId}");
        }

        public Task<UserFioDto> GetUserFioAsync(int userId)
        {
            return GetAsync<UserFioDto>("/GetUserFio", new {userId});
        }

        public Task SetUserFioAsync(UserFioDto dto)
        {
            return PostAsync("/SetUserFio", dto);
        }

        public Task<List<int>> GetFirmsWithActiveUserByFirmIdsAsync(IReadOnlyCollection<int> firmIds)
        {
            firmIds = firmIds.AsSet();
            return PostAsync<IReadOnlyCollection<int>, List<int>>("/V2/GetFirmsWithActiveUserByFirmIds", firmIds);
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<string> GetDiscardPasswordGuidAsync(DiscardPasswordTokenRequestDto requestDto)
        {
            return PostAsync<DiscardPasswordTokenRequestDto, string>($"/V2/GetDiscardPasswordGuid", requestDto);
        }

        public async Task<bool> IsPasswordRecoveryGuidVerifiedAsync(Guid guid)
        {
            var result = await GetAsync<bool>("/V2/IsPasswordRecoveryGuidVerified", new { guid });
            return result;
        }

        public Task<bool> IsLoginBusyAsync(string login)
        {
            return GetAsync<bool>("/V2/IsLoginBusy", new { login });
        }

        public Task<IReadOnlyDictionary<int, int?>> GetAdditionalRegionIdsByUserIdsAsync(IReadOnlyCollection<int> ids)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, int?>>(
                "/V2/GetAdditionalRegionIdsByUserIds",
                ids);
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
