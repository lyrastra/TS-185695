using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.FrameInfo;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BackofficeV2.Client.FrameInfo
{
    public interface IFrameInfoApiClient: IDI
    {
        /// <summary>
        /// Получить информацию о пользователе для фрейма по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Задача, которая вернет информацию о пользователе для фрейма</returns>
        Task<FrameInfoForUserResponseDto> GetFrameInfoForUserByIdAsync(int id);

        /// <summary>
        /// Получить информацию о пользователе для фрейма по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Задача, которая вернет информацию о пользователе для фрейма</returns>
        Task<FrameInfoForUserResponseDto> GetFrameInfoForUserByLoginAsync(string login);

        /// <summary>
        /// Получить информацию о фирме для фрейма по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор фирмы</param>
        /// <returns>Задача, которая вернет информацию о фирме для фрейма</returns>
        Task<FrameInfoForFirmResponseDto> GetFrameInfoForFirmByIdAsync(int id);

        /// <summary>
        /// Получить информацию о фирме для фрейма по индивидуальному номеру налогоплательщика
        /// </summary>
        /// <param name="inn">Индивидуальный номер налогоплательщика</param>
        /// <returns>Задача, которая вернет информацию о фирме для фрейма</returns>
        Task<FrameInfoForFirmResponseDto> GetFrameInfoForFirmByInnAsync(string inn);
    }
}