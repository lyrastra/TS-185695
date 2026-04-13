using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PrimaryDocuments;

namespace Moedelo.AccountingV2.Client.PrimaryDocuments
{
    /// <summary>
    /// Клиент для получения первичных и связанных документов по договору.
    /// </summary>
    public interface IContractPrimaryDocumentsApiClient
    {
        /// <summary>
        /// Возвращает список закрывающих (первичных) документов, связанных с указанным договором.
        /// </summary>
        /// <param name="firmId">Идентификатор фирмы.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="contractId">Идентификатор договора.</param>
        /// <returns>Список связанных первичных документов.</returns>
        Task<List<ProjectLinkedDocumentDto>> GetPrimaryDocumentsByContractIdAsync(int firmId, int userId, int contractId);

        /// <summary>
        /// Возвращает все счета, связанные с указанным договором.
        /// </summary>
        /// <param name="firmId">Идентификатор фирмы.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="contractId">Идентификатор договора.</param>
        /// <returns>Информация о счетах по договору.</returns>
        Task<BillsForContractDetailsDto> GetBillsByContractAsync(int firmId, int userId, int contractId);
    }
}