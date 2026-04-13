using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.FrameInfo;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BankIntegrationsV2.Client.FrameInfo
{
    public interface IBankIntegrationsFrameInfoClient : IDI
    {
        /// <summary>
        /// Получить информацию о банковской интеграции фирмы для фрейма по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор фирмы</param>
        /// <returns>Задача, которая вернет информации о банковской интеграции фирмы для фрейма</returns>
        Task<BankIntegrationsFrameInfoForFirmResponseDto> GetForFirmByIdAsync(int id);

        /// <summary>
        /// Получить информацию о банковской интеграции фирмы для фрейма по индивидуальному номеру налогоплательщика
        /// </summary>
        /// <param name="inn">Индивидуальный номер налогоплательщика</param>
        /// <returns>Задача, которая вернет информации о банковской интеграции фирмы для фрейма</returns>
        Task<BankIntegrationsFrameInfoForFirmResponseDto> GetForFirmByInnAsync(string inn);

        /// <summary>
        /// Получить информацию о банковских интеграциях фирм для фрейма по идентификаторам
        /// </summary>
        /// <param name="ids">Список идентификаторов фирм</param>
        /// <returns>Задача, которая вернет информацию о банковских интеграциях фирм для фрейма</returns>
        Task<List<BankIntegrationsFrameInfoForFirmResponseDto>> GetForFirmsByIdsAsync(List<int> ids);
    }
}