using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.TaxPostings;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.TaxPostings.Dto;

namespace Moedelo.TaxPostings.Client
{
    /// <summary>
    /// НУ-проводки для ОСНО
    /// </summary>
    public interface ITaxPostingsOsnoClient : IDI
    {
        /// <summary>
        /// Возвращает проводки по документу
        /// </summary>
        /// <param name="firmId">Фирма</param>
        /// <param name="userId">Пользователь</param>
        /// <param name="baseId">Идентификатор документа</param>
        /// <param name="filterBadOperationStates">Исключать проводки по п/п из "красной таблицы" (по умолчанию "true" по TS-85033)</param>
        Task<List<TaxPostingOsnoDto>> GetByBaseIdAsync(int firmId, int userId, long baseId, bool filterBadOperationStates = true);

        Task SaveAsync(int firmId, int userId, IReadOnlyCollection<TaxPostingOsnoDto> taxPostings);

        Task DeleteAsync(int firmId, int userId, long documentBaseId);

        Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task<List<DocumentTaxSumDto>> GetDocumentTaxSumsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIdList);

        /// <summary>
        /// Возвращает проводки по документам
        /// </summary>
        /// <param name="firmId">Фирма</param>
        /// <param name="userId">Пользователь</param>
        /// <param name="documentBaseIds">Идентификаторы документов</param>
        /// <param name="filterBadOperationStates">Исключать проводки по п/п из "красной таблицы" (по умолчанию "true" по TS-85033)</param>
        Task<List<TaxPostingOsnoDto>> GetByDocumentIdsAsync(
            int firmId,
            int userId,
            IReadOnlyCollection<long> documentBaseIds,
            bool filterBadOperationStates = true);

        /// <summary>
        /// Возвращает провоодки по периоду (+опциональные фильтры)
        /// </summary>
        /// <param name="firmId">Фирма</param>
        /// <param name="userId">Пользователь</param>
        /// <param name="startDate">Конец периода (включительно)</param>
        /// <param name="endDate">Начало периода (включительно)</param>
        /// <param name="filterBadOperationStates">Исключать проводки по п/п из "красной таблицы" (по умолчанию "true" по TS-85033)</param>
        /// <param name="excludeNormalizedCostType">Исключить указанный тип нормируемого расхода (опционально)</param>
        /// <param name="limit">Выбрать N записей (опционально)</param>
        Task<List<TaxPostingOsnoDto>> GetByPeriodAsync(
            int firmId,
            int userId,
            DateTime startDate,
            DateTime endDate,
            bool filterBadOperationStates = true,
            NormalizedCostType? excludeNormalizedCostType = null,
            int? limit = null);

        /// <summary>
        /// Обновляет суммы нормируемых расходов по кварталам 
        /// </summary>
        Task UpdateNormalizedSumForQuarterAsync(int firmId, int userId, IReadOnlyCollection<OsnoNormalizedSumForQuarterDto> request);
    }
}