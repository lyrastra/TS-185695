using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesStatements.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesStatements
{
    public interface IPurchasesStatementsApiClient
    {
        
        Task<DataPageResponse<DocsPurchasesStatementByCriteriaResponseDto>> GetByCriteriaAsync(
            DocsPurchasesStatementsByCriteriaRequestDto dto, 
            int? companyId = null);
        
        /// <summary>
        /// Возвращает список входящих актов без с/ф
        /// </summary>
        Task<IReadOnlyCollection<PurchasesStatementsWithItemsDto>> GetWithoutInvoices(DateTime startDate, DateTime endDate);
    }
}