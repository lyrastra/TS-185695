using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.ElectronicReportsV2.Dto.StatusInfo;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ElectronicReportsV2.Client.StatusInfo
{
    public interface IElectronicReportsStatusInfoClient : IDI
    {
        /// <summary>
        /// Получить информацию об электронной отчетности фирмы по индивидуальному номеру налогоплательщика
        /// </summary>
        /// <param name="inn">Индивидуальный номер налогоплательщика</param>
        /// <returns>Задача, которая вернет информации об электронной отчетности фирмы</returns>
        Task<ElectronicReportsStatusInfoForFirmResponseDto> GetForFirmByInnAsync(string inn);

        /// <summary>
        /// Получить информацию об электронных отчетностях фирм по идентификаторам
        /// </summary>
        /// <param name="firmIds">Список идентификаторов фирм</param>
        /// <returns>Задача, которая вернет информацию об электронных отчетностях фирм</returns>
        Task<List<ElectronicReportsStatusInfoForFirmResponseDto>> GetForFirmsByIdsAsync(IReadOnlyCollection<int> firmIds);
    }
}