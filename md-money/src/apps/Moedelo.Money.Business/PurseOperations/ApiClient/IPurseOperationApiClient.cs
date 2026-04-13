using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.Common.Domain.Models;

namespace Moedelo.Money.Business.PurseOperations.ApiClient
{
    internal interface IPurseOperationApiClient
    {
        Task<T> GetAsync<T>(string path) where T : class;

        /// <summary>
        /// Получает статусы документов по коллекции идентификаторов базовых документов.
        /// </summary>
        /// <param name="documentBaseIds">
        /// Коллекция идентификаторов базовых документов, для которых требуется получить статусы.
        /// </param>
        /// <param name="setting">
        /// Дополнительные HTTP-настройки запроса (необязательный параметр).
        /// </param>
        /// <returns>
        /// Асинхронная задача, возвращающая список статусов документов.
        /// </returns>
        Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds, 
            HttpQuerySetting setting = null);
    }
}
