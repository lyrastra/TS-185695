using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.FrameInfo;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BillingV2.Client.FrameInfo
{
    public interface IBillingFrameInfoClient : IDI
    {
        /// <summary>
        /// Получить информацию о биллинге фирмы для фрейма по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор фирмы</param>
        /// <returns>Задача, которая вернет информации о биллинге фирмы для фрейма</returns>
        Task<BillingFrameInfoForFirmResponseDto> GetForFirmByIdAsync(int id);

        /// <summary>
        /// Получить информацию о биллинге фирмы для фрейма по индивидуальному номеру налогоплательщика
        /// </summary>
        /// <param name="inn">Индивидуальный номер налогоплательщика</param>
        /// <returns>Задача, которая вернет информации о биллинге фирмы для фрейма</returns>
        Task<BillingFrameInfoForFirmResponseDto> GetForFirmByInnAsync(string inn);

        /// <summary>
        /// Получить информацию о биллингах фирм для фрейма по идентификаторам
        /// </summary>
        /// <param name="ids">Список идентификаторов фирм</param>
        /// <returns>Задача, которая вернет информацию о биллингах фирм для фрейма</returns>
        Task<List<BillingFrameInfoForFirmResponseDto>> GetForFirmsByIdsAsync(List<int> ids);

        /// <summary>
        /// Получить историю платежей для фрейма
        /// </summary>
        Task<List<BillingFrameInfoPayHistoryDto>> GetPayHistoryByFirmId(int firmId);

        /// <summary>
        /// Получить историю платежей для фрейма
        /// </summary>
        Task<List<BillingFrameInfoPayHistoryDto>> GetPayHistoryByFirmIds(List<int> firmIds);

        /// <summary>
        /// Получить историю платежей для фрейма
        /// </summary>
        Task<List<BillingFrameInfoPayHistoryDto>> GetPayHistoryByInn(string inn);
    }
}