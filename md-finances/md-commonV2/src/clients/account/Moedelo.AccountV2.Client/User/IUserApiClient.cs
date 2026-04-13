using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.Requisites;
using Moedelo.AccountV2.Dto.User;

namespace Moedelo.AccountV2.Client.User;

public interface IUserApiClient
{
    /// <summary> 
    /// Возвращает базовую информацию о пользователе 
    /// </summary>
    Task<BaseUserInfoDto> GetBaseUserInfoAsync(int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить логин пользователя по его идентификатору
    /// </summary>
    /// <param name="userId">идентификатор пользователя</param>
    /// <returns>логин пользователя (null, если нет)</returns>
    Task<UserLoginDto> GetUserLoginAsync(int userId);

    Task<List<BaseUserInfoDto>> GetBaseUserInfoListAsync(IReadOnlyCollection<int> userIds);

    Task<List<BaseUserInfoDto>> GetBaseUserInfoListByLoginsAsync(IReadOnlyCollection<string> logins);

    Task<BaseUserInfoDto> GetBaseUserInfoByFirmIdAsync(int firmId);

    Task<List<BaseUserInfoDto>> GetBaseUserInfoListByFirmIdsAsync(IReadOnlyCollection<int> firmIds);

    Task<List<FirmLoginDto>> GetUserLoginsByFirmIdsAsync(IReadOnlyCollection<int> firmIds);

    Task<AdditionalUserInfoDto> GetAdditionalUserInfoAsync(int userId, CancellationToken cancellationToken = default);

    Task<List<UserIdFirmIdResponseDto>> GetAllFirmUserIdsAndFirmIdsByProductPlatformAsync(string productPlatform,
        bool onlyPaid);

    Task<List<UserIdFirmIdResponseDto>> GetAllFirmUserIdsAndFirmIdsByUserIdsAsync(IReadOnlyCollection<int> userIds);

    Task<List<UserDto>> GetByIdsAsync(IReadOnlyCollection<int> userIds);

    Task<IReadOnlyCollection<UserDto>> GetByAccountIdsAsync(
        IReadOnlyCollection<int> accountIds,
        CancellationToken cancellationToken = default);

    Task<UserDto> GetByLoginAsync(string login, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает список пользователей, начинающихся на <paramref name="loginPart"/>
    /// </summary>
    /// <param name="loginPart">часть логина, по которой осуществляется поиск</param>
    /// <param name="count">количество возвращаемых записей. Допустимое значение: от 1 до 10</param>
    Task<List<UserDto>> GetUsersByLoginAutocompleteAsync(string loginPart, int count);

    Task<int?> GetFirmIdByLoginAsync(string login);

    Task<CreateUserPartnerResponseDto> CreateSberPartnerAsync(UserPartnerDto user);

    Task<int> CreateAsync(UserDto user);

    Task<bool> CheckPasswordAsync(int userId, string password);

    /// <summary>
    /// Сменить логин пользователя без подтверждения и уведомления пользователя.
    /// </summary>
    /// <param name="requestDto">параметры запроса</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <returns>результат смены</returns>
    public Task<LoginChangeResponseDto> ChangeLoginWithoutConfirmationAsync(
        LoginChangeRequestDto requestDto, CancellationToken cancellationToken);

    /// <summary>
    /// Разместить заявку на смену логина пользователя с подтверждением по почте
    /// </summary>
    Task<LoginChangeWithEmailConfirmationResponseDto> RequestLoginChangingAsync(
        LoginChangeWithEmailConfirmationRequestDto requestDto,
        CancellationToken cancellationToken);

    /// <summary>
    /// Разместить заявку на первую смену логина пользователя с подтверждением по почте
    /// </summary>
    Task<FirstLoginChangeWithEmailConfirmationResponseDto> RequestLoginChangingAsync(
        FirstLoginChangeWithEmailConfirmationRequestDto requestDto,
        CancellationToken cancellationToken);

    /// <summary>
    /// Подтвердить смену логина, предъявив код подтверждения
    /// </summary>
    Task<ConfirmUserLoginChangingResponseDto> ConfirmLoginChangingByCodeAsync(
        ConfirmUserLoginChangingDto requestDto,
        CancellationToken cancellationToken);

    /// <summary>
    /// Подтвердить первую смену логина, предъявив код подтверждения
    /// </summary>
    Task<ConfirmUserLoginChangingResponseDto> ConfirmLoginChangingByCodeAsync(
        ConfirmUserFirstLoginChangingDto requestDto,
        CancellationToken cancellationToken);

    Task<BaseUserResponseDto> PasswordRecoveryAsync(PasswordRecoveryRequestDto requestDto);

    Task<GetUtmFieldsV2Dto> GetUtmFieldsByUserIdAsync(int userId);

    Task<bool> CanUserAccessToFirmAsync(int firmId, int userId);

    /// <summary>
    /// Возвращает идентификатор фирмы по умолчанию для пользователя.
    /// Если для пользователя невозможно определить фирму по умолчанию, то завершается с исключением.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>идентификатор фирмы (если невозможно определить выбрасывается исключение)</returns>
    Task<int> GetDefaultFirmIdAsync(int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает идентификатор фирмы по умолчанию для пользователя.
    /// Если для пользователя невозможно определить фирму по умолчанию, то возвращается 0.
    /// </summary>
    /// <param name="userId">идентификатор пользователя</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <returns>идентификатор фирмы (0 - невозможно определить)</returns>
    Task<int> GetDefaultFirmIdOrZeroAsync(int userId, CancellationToken cancellationToken);

    Task<AdminRequisitesDto> GetAdminRequisitesAsync(int userId);

    Task<BaseUserInfoDto> GetOwnerByCurrentFirmIdAsync(int firmId, CancellationToken cancellationToken = default);

    Task SetUtmFieldsAsync(SetUtmFieldsDto dto);

    Task<bool> IsDeletedAsync(int userId);

    Task<IReadOnlyDictionary<int, bool>> GetFlagsIsDeletedForUserIdsAsync(
        IReadOnlyCollection<int> userIds,
        CancellationToken cancellationToken = default);

    Task<bool> MarkAsDeletedAsync(int userId);

    Task<UserFioDto> GetUserFioAsync(int userId);

    Task SetUserFioAsync(UserFioDto dto);

    Task<List<int>> GetFirmsWithActiveUserByFirmIdsAsync(IReadOnlyCollection<int> firmIds);

    /// <summary>
    /// Позволяет получить guid сброса пароля, действующий до даты N указанной в модели
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns>Guid в формате 'N' (без дефисов)</returns>
    Task<string> GetDiscardPasswordGuidAsync(DiscardPasswordTokenRequestDto requestDto);

    /// <summary>
    /// Проверяет, не был ли использован guid для задания пароля
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<bool> IsPasswordRecoveryGuidVerifiedAsync(Guid guid);

    /// <summary>
    /// Проверяет, занят ли логин
    /// </summary>
    /// <param name="login">login</param>
    /// <returns>true/false</returns>
    Task<bool> IsLoginBusyAsync(string login);

    Task<IReadOnlyDictionary<int, int?>> GetAdditionalRegionIdsByUserIdsAsync(IReadOnlyCollection<int> userIds);

    Task<IReadOnlyCollection<int>> GetSourceIdsByTargetUserIdsAsync(
        IReadOnlyCollection<int> targetUserIds,
        CancellationToken cancellationToken = default);
}
