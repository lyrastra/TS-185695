using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.FrameInfo;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountV2.Client.FrameInfo
{
    public interface IAccountFrameInfoApiClient : IDI
    {
        /// <summary>
        /// Получить часть информации о пользователе со списком идентификаторов фирм для фрейма по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Задача, которая вернет часть информации о пользователе со списком идентификаторов фирм для фрейма</returns>
        Task<AccountFrameInfoForUserWithFirmIdsResponseDto> GetForUserWithFirmIdsByIdAsync(int id);

        /// <summary>
        /// Получить часть информации о пользователе со списком идентификаторов фирм для фрейма по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Задача, которая вернет часть информации о пользователе со списком идентификаторов фирм для фрейма</returns>
        Task<AccountFrameInfoForUserWithFirmIdsResponseDto> GetForUserWithFirmIdsByLoginAsync(string login);

        /// <summary>
        /// Получить часть информации о фирме для фрейма по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор фирмы</param>
        /// <returns>Задача, которая вернет часть информации о фирме для фрейма</returns>
        Task<AccountFrameInfoForFirmResponseDto> GetForFirmByIdAsync(int id);

        /// <summary>
        /// Получить часть информации о фирме для фрейма по индивидуальному номеру налогоплательщика
        /// </summary>
        /// <param name="inn">Индивидуальный номер налогоплательщика</param>
        /// <returns>Задача, которая вернет часть информации о фирме для фрейма</returns>
        Task<AccountFrameInfoForFirmResponseDto> GetForFirmByInnAsync(string inn);

        /// <summary>
        /// Получить части информаций о фирмах для фрейма по идентификатору
        /// </summary>
        /// <param name="ids">Идентификаторы фирм</param>
        /// <returns>Задача, которая вернет части информаций о фирмах для фрейма</returns>
        Task<List<AccountFrameInfoForFirmResponseDto>> GetForFirmsByIdsAsync(IReadOnlyCollection<int> ids);
    }
}