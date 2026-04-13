using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.Patent;

namespace Moedelo.RequisitesV2.Client.Patent
{
    public interface IPatentApiClient : IDI
    {
        /// <summary>
        /// Возвращает список всех патентов в фирме @firmId
        /// </summary>
        Task<List<PatentDto>> GetAllAsync(int firmId, int userId, int? year);

        /// <summary>
        /// Возвращает список всех патентов в фирме @firmId (без некоторых полей)
        /// </summary>
        Task<List<PatentDto>> GetWithoutAdditionalDataAsync(int firmId, int userId, int? year);

        /// <summary>
        /// Возвращает патент по Id
        /// </summary>
        Task<PatentDto> GetAsync(int firmId, int userId, long patentId);

        /// <summary>
        /// Возвращает патент по Id (без некоторых полей)
        /// </summary>
        Task<PatentDto> GetWithoutAdditionalDataByIdAsync(int firmId, int userId, long patentId);

        /// <summary>
        /// Возвращает патент по списку Id
        /// </summary>
        Task<List<PatentDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> patentIds);

        /// <summary>
        /// Возвращает патент по wizardId
        /// </summary>
        Task<PatentDto> GetByWizardIdAsync(int firmId, int userId, long wizardId);

        Task<List<CodeKindOfBusinessDto>> GetAllCodeKindOfBusinessAsync(int firmId, int userId);

        Task<List<BudgetaryPatentAutocompleteDto>> BudgetaryPatentAutocompleteAsync(int firmId, int userId, int count, string query, DateTime? documentDate);

        /// <summary>
        /// Проверяет существоание патента у фирмы по Id
        /// </summary>
        Task<bool> IsExistsAsync(int firmId, int userId, long patentId);

        /// <summary>
        /// Проверяет наличие патентов у фирмы
        /// </summary>
        Task<bool> IsAnyExistsAsync(int firmId, int userId, int year);

        /// <summary>
        /// Сохранение нового патента
        /// </summary>
        Task<long> SaveAsync(int firmId, int userId, PatentDto dto);
    }
}
