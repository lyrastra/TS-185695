using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesWaybills.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesWaybills
{
    public interface IPurchasesWaybillsApiClient
    {
        Task<DataPageResponse<DocsPurchasesWaybillByCriteriaResponseDto>> GetByCriteriaAsync(
            DocsPurchasesWaybillsByCriteriaRequestDto dto, 
            int? companyId = null);
        
        /// <summary>
        /// Возвращает список входящих накладных без с/ф
        /// </summary>
        Task<IReadOnlyCollection<PurchasesWaybillWithItemsDto>> GetWithoutInvoices(DateTime startDate, DateTime endDate);
    }
}