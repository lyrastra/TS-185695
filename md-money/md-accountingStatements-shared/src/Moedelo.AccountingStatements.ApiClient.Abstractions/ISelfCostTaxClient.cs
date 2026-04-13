using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos.SelfCostTax;
using Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos.TaxSelfCost;

namespace Moedelo.AccountingStatements.ApiClient.Abstractions
{
    /// <summary>
    /// Клиент для бух. справок по себестоимости НУ (ФИФО)
    /// </summary>
    public interface ISelfCostTaxClient
    {
        Task<SelfCostTaxCreateResponseDto[]> CreateAsync(IReadOnlyCollection<SelfCostTaxCreateRequestDto> requests);
        
        Task DeleteByPeriodAsync(SelfCostTaxDeleteByPeriodDto request);

        Task<SelfCostTaxGetResponseDto> GetByPeriodAsync(SelfCostTaxGetByPeriodDto request);
    }
}
