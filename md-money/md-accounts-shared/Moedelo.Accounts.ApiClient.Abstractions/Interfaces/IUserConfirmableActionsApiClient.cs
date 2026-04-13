using System.Threading;
using System.Threading.Tasks;
using Moedelo.Accounts.Abstractions.Dto.UserActions;

namespace Moedelo.Accounts.Abstractions.Interfaces;

public interface IUserConfirmableActionsApiClient
{
    /// <summary>
    /// Создать новое действие пользователя, требующее подтверждения
    /// </summary>
    /// <param name="dto">параметры действия</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <returns>параметры созданного действия</returns>
    Task<CreatedUserConfirmableActionDto> CreateNewAsync(NewUserConfirmableActionDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    /// Подтвердить действие пользователя
    /// </summary>
    /// <param name="dto">параметры подтверждения</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <returns>результат подтверждения (см. <see cref="UserConfirmableActionConfirmationResultDto.Status"/>)</returns>
    Task<UserConfirmableActionConfirmationResultDto> ConfirmAsync(UserConfirmableActionConfirmationDto dto,
        CancellationToken cancellationToken);
}
