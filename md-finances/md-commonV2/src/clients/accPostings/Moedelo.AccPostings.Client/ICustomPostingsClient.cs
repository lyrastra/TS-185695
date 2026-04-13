using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccPostings.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AccPostings.Client
{
    public interface ICustomPostingsClient : IDI
    {
        /// <summary>
        /// Получить кастомные проводки по  указанному критерию поиска
        /// </summary>
        Task<List<AccountingPostingDto>> GetByAsync(int firmId, int userId, AccountingPostingsSearchCriteriaDto criteria);

        /// <summary>
        /// Добавлен в качестве временного решения для перехода проекта ЗП на новый клиет.
        /// Перевести получение проводок на BaseId документа (нужен рефакторинг схемы данных)
        /// </summary>
        Task<List<AccountingPostingDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> potingIds);
        
        /// <summary>
        /// Сохранить кастомные проводки для документа по его documentBaseId
        /// </summary>
        Task SaveAsync(int firmId, int userId, long documentBaseId, IReadOnlyCollection<CustomPostingDescriptionDto> customPostings);

        /// <summary>
        /// Удалить кастомные проводки для документа по его documentBaseId
        /// </summary>
        Task DeleteByDocumentAsync(int firmId, int userId, long documentBaseId);

        /// <summary>
        /// Удалить кастомные проводки для списка документов
        /// </summary>
        Task DeleteByDocumentsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);
    }
}