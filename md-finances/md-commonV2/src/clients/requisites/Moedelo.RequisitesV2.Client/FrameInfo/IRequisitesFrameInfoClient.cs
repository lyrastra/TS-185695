using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.FrameInfo;

namespace Moedelo.RequisitesV2.Client.FrameInfo
{
    public interface IRequisitesFrameInfoClient : IDI
    {
        /// <summary>
        /// Получить информацию о реквизитах для фирмы для фрейма по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор фирмы</param>
        /// <returns>Задача, которая вернет информации о реквизитах фирмы для фрейма</returns>
        Task<RequisitesFrameInfoForFirmResponseDto> GetForFirmByIdAsync(int id);

        /// <summary>
        /// Получить информацию о реквизитах фирмы для фрейма по индивидуальному номеру налогоплательщика
        /// </summary>
        /// <param name="inn">Индивидуальный номер налогоплательщика</param>
        /// <returns>Задача, которая вернет информации о реквизитах фирмы для фрейма</returns>
        Task<RequisitesFrameInfoForFirmResponseDto> GetForFirmByInnAsync(string inn);

        /// <summary>
        /// Получить информацию о реквизитах фирм для фрейма по идентификаторам
        /// </summary>
        /// <param name="ids">Список идентификаторов фирм</param>
        /// <returns>Задача, которая вернет информацию о реквизитах фирм для фрейма</returns>
        Task<List<RequisitesFrameInfoForFirmResponseDto>> GetForFirmsByIdsAsync(List<int> ids);
    }
}