using Moedelo.Authorities.ApiClient.Abstractions.Sfr.Dto;
using System.Threading.Tasks;

namespace Moedelo.Authorities.ApiClient.Abstractions.Sfr
{
    public interface ISfrFirmRequisitesApiClient
    {
        /// <summary>
        /// Получение настроек фонда СФР.
        /// Реализован для Core и NetFramework.
        /// </summary>
        /// <param name="firmId">для поддержки NetFramework клиентов</param>
        /// <param name="userId">для поддержки NetFramework клиентов</param>
        /// <returns></returns>
        Task<SfrFirmRequisitesResponseDto> GetAsync(int firmId, int userId);

        /// <summary>
        /// Сохранение/Обновление настроек фонда СФР.
        /// Реализован для Core и NetFramework.
        /// </summary>
        /// <param name="firmId">для поддержки NetFramework клиентов</param>
        /// <param name="userId">для поддержки NetFramework клиентов</param>
        /// <param name="saveRequest">Настройки фонда СФР</param>
        /// <returns></returns>
        Task SaveAsync(int firmId, int userId, SfrFirmRequisitesSaveRequestDto saveRequest);

        /// <summary>
        /// Автоманическая актуализация Регистрационого номера в СФР (ПФР) и Регистрационного номера в СФР (ФСС).
        /// Реализован только для Core.
        /// </summary>
        /// <param name="numbers">рег. номера в СФР (ПФР и ФСС)</param>
        /// <returns></returns>
        Task ResetRegNumbersAsync(RegNumbersDto numbers);

        /// <summary>
        /// Удаление настроек фонда СФР
        /// Реализован для Core и NetFramework.
        /// </summary>
        /// <param name="firmId">для поддержки NetFramework клиентов</param>
        /// <param name="userId">для поддержки NetFramework клиентов</param>
        /// <returns></returns>
        Task DeleteAsync(int firmId, int userId);
    }
}
