using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesUpds.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesUpds
{
    public interface IPurchasesUpdsApiClient
    {
        Task<DataPageResponse<DocsPurchasesUpdByCriteriaResponseDto>> GetByCriteriaAsync(
            DocsPurchasesUpdsByCriteriaRequestDto criteria, 
            int? companyId = null);
        
        /// <summary>
        /// Возвращает список УПД со статусом 2 без с/ф
        /// </summary>
        Task<IReadOnlyCollection<PurchasesUpdWithItemsDto>> GetWithoutInvoices(DateTime startDate, DateTime endDate);
    }
}