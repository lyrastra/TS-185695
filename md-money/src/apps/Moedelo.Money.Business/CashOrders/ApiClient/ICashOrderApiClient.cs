using Moedelo.Money.CashOrders.Dto.CashOrders;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.Common.Domain.Models;

namespace Moedelo.Money.Business.CashOrders.ApiClient
{
    internal interface ICashOrderApiClient
    {
        Task<T> GetAsync<T>(string path) where T : class;

        Task CreateAsync<T>(string path, T dto) where T : class;

        Task UpdateAsync<T>(string path, T dto) where T : class;
        Task<TResponse> UpdateAsync<TRequest, TResponse>(string path, TRequest dto)
            where TRequest : class;

        Task DeleteAsync(string path);
        Task<TResponse> DeleteAsync<TResponse>(string path);

        Task<OperationTypeDto[]> GetOperationTypeByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);

        /// <summary>
        /// Получает статусы документов по указанным идентификаторам базовых документов.
        /// </summary>
        /// <param name="documentBaseIds">
        /// Коллекция идентификаторов базовых документов, для которых необходимо получить статусы.
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
