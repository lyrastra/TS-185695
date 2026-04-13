using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Domain.Operations;

namespace Moedelo.Money.Business.Abstractions.Operations
{
    public interface IOperationsValidationService
    {
        /// <summary>
        /// Получает статусы проверки документов по базовым идентификаторам на основании переданных параметров запроса.
        /// </summary>
        /// <param name="request">
        /// Объект запроса, включающий идентификаторы базовых документов, флаг проверки сотрудником аутсорсера,
        /// а также статус оплаты.
        /// </param>
        /// <returns>
        /// Асинхронная задача, возвращающая список статусов проверки документов.
        /// </returns>
        Task<IReadOnlyList<DocumentValidationStatus>> GetDocumentsStatusByBaseIdsAsync(DocumentsStatusRequest request);
    }
}